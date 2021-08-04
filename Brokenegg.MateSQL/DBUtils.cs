using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Brokenegg.MateSQL
{
    public class DBUtils
    {
        public static String CammelCaseToDB(String Name)
        {
            var result = Regex.Replace(Name, "(?<=[a-z0-9])[A-Z]", m => "_" + m.Value);
            return result.ToLowerInvariant();
        }

        public static Object AdaptFieldValueToDB(Object Value, Type FieldType = null)
        {
            Type MyType = ( FieldType == null ? Value.GetType() : FieldType);

            if (typeof(String) == MyType ||
                typeof(DateTime) == MyType) 
            {
                return String.Format("'{0}'", Value);
            }
            else if (typeof(Boolean) == MyType)
            {
                return ((Boolean)Value).ToString().ToLower();
            }

            return Value;
        }
    }
}
