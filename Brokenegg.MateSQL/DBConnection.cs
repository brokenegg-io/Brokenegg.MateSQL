using Brokenegg.MateSQL.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Brokenegg.MateSQL
{
    public class DBConnection
    {
         public string Password { get; set; }
        public MySqlConnection Connection { get; set; }
     
        public bool IsConnect()
        {
            try
            {
                bool result = true;
                if (Connection == null)
                {
                    if (String.IsNullOrEmpty(DBConfig.Database))
                    {
                        result = false;
                    }
                    else
                    {
                        string connstring = GetDBString();
                        this.Connection = new MySqlConnection(connstring);
                        this.Connection.Open();
                        result = true;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ConnectionException(ex.Message);
            }
        }

        private string GetDBString(Boolean extra = false)
        {
            String extraCommandTemp = ";charset = utf8; convertzerodatetime = true;";
            String extraCommand = extra ? extraCommandTemp : String.Empty;

            return $"Server={DBConfig.Server}; database={DBConfig.Database}; UID={DBConfig.User}; password={DBConfig.Password} {extraCommand}";
        }

        public void Close()
        {
            this.Connection.Close();
        }
    }
}
