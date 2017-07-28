using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarckDX.MateSQL
{
    public class DBConnection
    {
        private DBConfig DatabaseConfig;

        public DBConnection(DBConfig DatabaseConfig)
        {
            this.DatabaseConfig = DatabaseConfig;
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;

        public static DBConnection Instance(DBConfig DatabaseConfig)
        {
            if (_instance == null)
                _instance = new DBConnection(DatabaseConfig);
            return _instance;
        }



        public bool IsConnect()
        {
            try
            {
                bool result = true;
                if (Connection == null)
                {
                    if (String.IsNullOrEmpty(this.DatabaseConfig.Database))
                    {
                        result = false;
                    }
                    else
                    {
                        string connstring = GetDBString();
                        connection = new MySqlConnection(connstring);
                        connection.Open();
                        result = true;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetDBString(Boolean extra = false)
        {
            String extraCommandTemp = ";charset = utf8; convertzerodatetime = true;";
            String extraCommand = extra ? extraCommandTemp : String.Empty;

            return string.Format("Server={0}; database={1}; UID={2}; password={3} {4}",
                     DatabaseConfig.Server, DatabaseConfig.Database, DatabaseConfig.User, DatabaseConfig.Password, extraCommand);
        }

        public void Close()
        {
            connection.Close();
            _instance = null;
        }
    }
}
