using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace NetCoreFrame.WebUI.Extensions
{
    public class SQLHelper
    {
        IConfiguration _configuration;
        private string _connectionString;
        public SQLHelper(IConfiguration configuration)
        {
            _configuration = configuration; 
            _connectionString = _configuration.GetConnectionString("CoreFrameContext");
        }
       
    }
}
