using DegreeWork.Common.ExternalApiUtils.Abstractions;
using DegreeWork.Common.ExternalApiUtils.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi
{
    internal class MicrosoftOAuthDataMarketAuthorizer : ApiAuthorizer
    {
        private const string AUTHORIZATION_SERVER_URI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private static readonly TimeSpan Epsilun = TimeSpan.FromMinutes(1);

        private static Lazy<MicrosoftOAuthDataMarketAuthorizer> instanse = new Lazy<MicrosoftOAuthDataMarketAuthorizer>();

        public static MicrosoftOAuthDataMarketAuthorizer Instanse
        {
            get { return instanse.Value; }
        }


        private ConcurrentDictionary<string, AccessTokenTaskModel> tokens;

        public MicrosoftOAuthDataMarketAuthorizer()
        {
            tokens = new ConcurrentDictionary<string, AccessTokenTaskModel>();
        }

        public override async Task AuthorizeRequest(WebRequest request)
        {
            string accessUri = request.RequestUri.GetLeftPart(UriPartial.Authority);
            AccessTokenTaskModel tokenModel;
            if(!tokens.TryGetValue(accessUri, out tokenModel))
            {
                tokenModel = tokens.AddOrUpdate(accessUri, 
                    s => GetActiveAccessTokenModel(s),
                    (s, t) => t
                );
            }

            if(tokenModel.NextUpdate.HasValue && 
                tokenModel.NextUpdate.Value.Subtract(Epsilun) < DateTime.Now) 
            {
                TaskCompletionSource<bool> taskSource = new TaskCompletionSource<bool>();
                AccessTokenTaskModel newTask = GetPendingAccessTokenModel(taskSource.Task, accessUri);
                bool updated = tokens.TryUpdate(accessUri, newTask, tokenModel);
                taskSource.SetResult(updated);
            }

            AdmAccessToken accessToken = await tokenModel.Task;
            request.Headers.Add("Authorization", "Bearer " + accessToken.access_token);
        }

        private AccessTokenTaskModel GetActiveAccessTokenModel(string accessUri)
        {
            TaskCompletionSource<bool> taskSource = new TaskCompletionSource<bool>();
            taskSource.SetResult(true);
            return GetPendingAccessTokenModel(taskSource.Task, accessUri);
        }

        private AccessTokenTaskModel GetPendingAccessTokenModel(Task<bool> shouldStart, string accessUri)
        {
            AccessTokenTaskModel result = new AccessTokenTaskModel();
            result.Task = shouldStart.ContinueWith<AdmAccessToken>(t => {
                if(t.Result)
                {
                    Task<AdmAccessToken> task = GetTokenAsync(accessUri);
                    return task.Result;
                }

                return null;
            });

            result.Task.ContinueWith(t => {
                if(t.Result != null)
                {
                    TimeSpan span = TimeSpan.FromSeconds(t.Result.expires_in);
                    result.NextUpdate = DateTime.Now.Add(span);
                }
            });

            return result;
        }

        private async Task<AdmAccessToken> GetTokenAsync(string accessUri)
        {
            ApiLoginModel loginData = ConfigurationKeys.MicrosoftOAuthLoginModel;
            string requestDetails = string.Format(
                "grant_type=client_credentials&client_id={0}&client_secret={1}&scope={2}",
                Uri.EscapeDataString(loginData.ClientId),
                Uri.EscapeDataString(loginData.ClientSecret),
                accessUri);

            WebRequest loginRequest = WebRequest.Create(AUTHORIZATION_SERVER_URI);
            loginRequest.ContentType = "application/x-www-form-urlencoded";
            loginRequest.Method = "POST";
            byte[] requestData = Encoding.ASCII.GetBytes(requestDetails);
            Stream requestStream = await loginRequest.GetRequestStreamAsync();
            using(requestStream)
                requestStream.Write(requestData, 0, requestData.Length);

            WebResponse response = await loginRequest.GetResponseAsync();
            string responseString;
            using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                responseString = await reader.ReadToEndAsync();

            AdmAccessToken result = JsonConvert.DeserializeObject(responseString, 
                typeof(AdmAccessToken)) as AdmAccessToken;

            return result;
        }

        private class AdmAccessToken
        {
            public string access_token;
            public string token_type;
            public int expires_in;
            public string scope;
        }

        private class AccessTokenTaskModel
        {
            public DateTime? NextUpdate;
            public Task<AdmAccessToken> Task;
        }
    }
}
