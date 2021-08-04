using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brokenegg.MateSQL
{
    public class DBKey
    {
        public String Name { get; set; }
        public Object Value { get; set; }

        public String Operator { get; set; }

        public DBKey(String Name, Object Value, String Operator)
        {
            this.Name = Name;
            this.Value = Value;
            this.Operator = Operator;
        }

        public DBKey(String Name, Object Value)
        {
            this.Name = Name;
            this.Value = Value;
            this.Operator = DBOperator.Equals;
        }

        
    }
}
