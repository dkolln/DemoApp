using DemoApp.Web.Hosting;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:" + args[0];

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(url);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", url);
            Console.ReadLine();
        }
    }
}
