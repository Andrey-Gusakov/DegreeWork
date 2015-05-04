using DegreeWork.WebServices;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.ConsoleStarter
{
    class Program
    {
        private const string baseUrl = "http://localhost:12345/";

        static void Main(string[] args)
        {
            using(WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine("Listening http://localhost:12345/...\nPress [Enter] to exit...");
                Console.ReadLine();
            }
        }
    }
}
