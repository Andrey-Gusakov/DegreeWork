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
        private const string baseUrl = "http://localhost:23456/";

        static void Main(string[] args)
        {
            using(WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine("Listening http://localhost:23456/...\nPress [Enter] to exit...");
                Console.ReadLine();
            }
        }
    }
}
