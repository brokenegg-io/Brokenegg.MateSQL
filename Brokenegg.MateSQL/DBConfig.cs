using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brokenegg.MateSQL
{
    public class DBConfig
    {

        public DBConfig(string server, string user, string password, string database)
        {
            DBConfig.Server = server;
            DBConfig.User = user;
            DBConfig.Password = password;
            DBConfig.Database = database;
        }

        public static String Server { get; set; }
        public static String User { get; set; }
        public static String Password { get; set; }
        public static String Database { get; set; }
    }
}
