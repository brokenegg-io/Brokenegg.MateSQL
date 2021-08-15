using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Brokenegg.MateSQL
{
    public class DBModel
    {
        public Int32 Id { get; set; }
        protected String TableName { get; set; }
        private Type MyType 
        {
            get
            {
                return this.GetType();
            }
        }
        private DBConfig Config { get; set; }
        private DBCommand Command { get; set; }

        public DBModel() 
        {
            this.TableName = DBUtils.CammelCaseToDB(MyType.Name);
            this.Config = null;
        }
        public DBModel(String TableName)
        {
            this.TableName = TableName;
            this.Config = null;
        }

        public Int32 Insert()
        {
            DBCommand Command = DBCommand.GenerateInsert(this, this.MyType, this.TableName);
            return Command.ExecuteInsert(new DBConfig());
        }
        
        public List<T> Get<T>(Int32 Id)
        {
            return Get<T>(Id, this.Config);
        }
        public List<T> Get<T>(Int32 Id, DBConfig Config)
        {
            List<DBKey> Where = new List<DBKey> { new DBKey("id", Id) };
            DBCommand Command = DBCommand.GenerateSelect(this, this.TableName, Where);
            return Command.ExecuteSelect<T>(Config);
        }

        public List<T> Select<T>()
        {
            return Select<T>(null);
        }
        public List<T> Select<T>(List<DBKey> Where)
        {
            return Select<T>(Where, this.Config);
        }
        public List<T> Select<T>(List<DBKey> Where, DBConfig Config)
        {
            DBCommand Command = DBCommand.GenerateSelect(this, this.TableName, Where);
            return Command.ExecuteSelect<T>(Config);
        }
    
        public void Select(DBModel Model)
        {

        }
        public void SetDbConfig(DBConfig Config)
        {
            this.Config = Config;
        }

    }
}
