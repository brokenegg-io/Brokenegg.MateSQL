using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Brokenegg.MateSQL
{
    public class DBCommand
    {
        public String SQLCommand { get; set; }

        public DBCommand(String SQLCommand)
        {
            this.SQLCommand = SQLCommand;
        }

        #region SQL Execute Commands
        
        public List<T> ExecuteSelect<T>(DBConfig Config)
        {
            List<T> objects = new List<T>();
            MySqlDataReader DataReader = RunQueryCommand(Config, SQLCommand);

            DataTable dtCustomers = new DataTable();
            dtCustomers.Load(DataReader);

            foreach (DataRow row in dtCustomers.Rows)
            {
                T instance = Activator.CreateInstance<T>();

                var bindingFlags = BindingFlags.Instance | BindingFlags.Public;

                
                foreach (var Field in instance.GetType().GetProperties(bindingFlags))
                {
                    if (row.Table.Columns.Contains(Field.Name))
                    {
                        var propInfo = instance.GetType().GetProperty(Field.Name);
                        if (propInfo != null)
                        {
                            propInfo.SetValue(instance, TypeConverter.ConvertTo(row[DBUtils.CammelCaseToDB(Field.Name)], Field.PropertyType), null);
                        }                        
                    }
                }

                objects.Add(instance);

            }

            DataReader.Close();

            return objects;
        }


        public Int32 ExecuteInsert(DBConfig Config)
        {
            return RunNoQueryCommand(Config, SQLCommand);
        }
        
        #endregion

        public static DBCommand GenerateInsert(DBModel Model, Type MyType, String TableName)
        {
            String Fields = String.Empty, Values = String.Empty;

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            Boolean IsFirstLine = true;

            foreach (var Field in MyType.GetProperties(bindingFlags))
            {
                if (Field.GetValue(Model, null) != null)
                {
                    Fields = String.Format("{0}{1}{2}", Fields, IsFirstLine ? String.Empty : Constants.Comma, DBUtils.CammelCaseToDB(Field.Name));
                    Values = String.Format("{0}{1}{2}", Values, IsFirstLine ? String.Empty : Constants.Comma, DBUtils.AdaptFieldValueToDB(Field.GetValue(Model, null)));

                    IsFirstLine = false;
                }
            }

            return new DBCommand(String.Format("INSERT INTO {0} ({1}) VALUES ({2})", TableName, Fields, Values));
        }

        public static DBCommand GenerateSelect(DBModel dBModel, String TableName, List<DBKey> Where = null)
        {
            String WhereString = String.Empty;
            Boolean IsFirstLine = true;

            if(Where!=null){
                if(Where.Count > 0){
                    WhereString =  " WHERE ";
                }
           

                foreach (DBKey Key in Where)
	            {
		            WhereString =  String.Format("{0} {1} {2} {3} {4}", WhereString, IsFirstLine ? String.Empty : Constants.And , DBUtils.CammelCaseToDB(Key.Name), Key.Operator ,DBUtils.AdaptFieldValueToDB(Key.Value));
                    IsFirstLine = false;
	            }
            }

            return new DBCommand(String.Format("SELECT * FROM {0} {1}", TableName, WhereString));
        }

        #region MySQL
        public static MySqlDataReader RunQueryCommand(DBConnection dbCon, String SQLCommand)
        {
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(SQLCommand, dbCon.Connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }

            return null;
        }

        public static Int32 RunNoQueryCommand(DBConnection dbCon, String SQLCommand)
        {
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(SQLCommand, dbCon.Connection);
                cmd.ExecuteNonQuery();
                return (Int32)cmd.LastInsertedId;
            }

            return -1;
        }

        public static MySqlDataReader RunQueryCommand(DBConfig Config, String SQLCommand)
        {
            var dbCon = DBConnection.Instance(Config);
            return RunQueryCommand(dbCon, SQLCommand);
        }

        public static Int32 RunNoQueryCommand(DBConfig Config, String SQLCommand)
        {
            var dbCon = DBConnection.Instance(Config);
            return RunNoQueryCommand(dbCon, SQLCommand);
        }

        #endregion
    }
}
