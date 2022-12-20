using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.DAL.Context
{
    public class AppConfigrations
    {
        public AppConfigrations()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSettion = root.GetSection("ConnectionStrings:DefaultConnection");
            sqlConnectionString = appSettion.Value;
        }
        public string sqlConnectionString { get; set; }


    }
}
