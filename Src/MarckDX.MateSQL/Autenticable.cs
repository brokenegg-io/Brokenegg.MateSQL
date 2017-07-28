using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarckDX.MateSQL
{
    public class Autenticable : DBModel
    {

        public String[] AuthParams = 
        {
            "Email", "Password"
        };

        public T Autenticate<T>(List<DBKey> Params)
        {
            return Select<T>(Params).FirstOrDefault();
        }

        public T Autenticate<T>(String Username, String Password)
        {
            List<DBKey> Params = new List<DBKey>
            {
                new DBKey(AuthParams[0], Username),
                new DBKey(AuthParams[1], Password)
            };

            return Select<T>(Params).FirstOrDefault();
        }

        public T Autenticate<T>(String Username, String Password, DBConfig Config)
        {
            List<DBKey> Params = new List<DBKey>
            {
                new DBKey(AuthParams[0], Username),
                new DBKey(AuthParams[1], Password)
            };

            return Select<T>(Params, Config).FirstOrDefault();
        }
    }
}
