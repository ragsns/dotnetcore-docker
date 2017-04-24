using System;
using Microsoft.AspNetCore.Hosting;
using MySql.Data.MySqlClient;
 
namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseStartup<Startup>()
            .Build();
 
            host.Run();
        }
    }
}
