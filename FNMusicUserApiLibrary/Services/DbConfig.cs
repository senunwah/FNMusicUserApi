using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNMusicUserApiLibrary.Services
{
    public class DbConfig
    {
        private static IConfiguration Configuration;

        public DbConfig(IConfiguration configuration) => Configuration = configuration;

        public static IDbConnection Connection
        {
            
            get
            {
                return new SqlConnection("Data Source=ENUNWAH-PC\\SQLEXPRESS;Initial Catalog=FNMusicDb;Integrated Security=True");
            }            
        }  
    }
}
