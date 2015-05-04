using DegreeWork.BusinessLogic.ExternalApi.ApiResultFactories;
using DegreeWork.Common.ExternalApiUtils.Abstractions;
using DegreeWork.Common.ExternalApiUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ApiInvokers
{
    class ImageProviderApiInvoker : ApiInvoker
    {
        private const string IMAGES_RESOURCE_PATTERN =
            "https://api.datamarket.azure.com/Bing/Search/Image?$format=json&Query=%27{0}%27&Adult=%27Off%27&ImageFilters=%27Size%3AMedium%2BAspect%3ASquare%27&$top=6";

        public ImageProviderApiInvoker(ImageApiResultFactory imageResultFactory) : base(imageResultFactory) { }

        public override bool IsRequired
        {
            get { return false; }
        }

        protected override WebRequest GetWebRequest(string word)
        {
            string requestUri = String.Format(IMAGES_RESOURCE_PATTERN, Uri.EscapeDataString(word));
            WebRequest request = WebRequest.Create(requestUri);

            ApiLoginModel loginData = ConfigurationKeys.MicrosoftAccountKeyLoginModel;
            byte[] authorizationData = ASCIIEncoding.ASCII.GetBytes(loginData.ClientId + ":" + loginData.ClientSecret);
            string base64Login = Convert.ToBase64String(authorizationData);

            request.Headers.Add("Authorization", "Basic " + base64Login);
            return request;
        }
    }
}
