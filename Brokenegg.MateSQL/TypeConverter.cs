using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brokenegg.MateSQL
{
    public class TypeConverter
    {
        public static T ConvertValue<T>(Object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }


        public static IList CreateList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        public static object ConvertTo(Object p, Type type)
        {
            return Convert.ChangeType(p, type);
        }
    }
}
