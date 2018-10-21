using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseracts.DMS.Data;

namespace Tesseracts.DMS.Logic
{
    public static class DatabaseHelper
    {
        private static IDictionary<string, string> _databaseSettings;

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Entities"].ConnectionString;
            }
        }

        public static string DocumentFolder
        {
            get
            {
                return Settings["DocumentFolder"];
            }
        }

        public static IDictionary<string, string> Settings
        {
            get
            {
                if (_databaseSettings == null)
                {
                    _databaseSettings = new Dictionary<string, string>();
                    using (var db = new Entities(DatabaseHelper.ConnectionString))
                    {
                        foreach (var item in db.Settings)
                        {
                            _databaseSettings[item.Name] = item.Value;
                        }
                    }
                }
                return _databaseSettings;
            }
        }


    }
}
