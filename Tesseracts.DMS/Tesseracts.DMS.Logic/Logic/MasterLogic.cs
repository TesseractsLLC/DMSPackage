using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseracts.DMS.Common;
using Tesseracts.DMS.Data;

namespace Tesseracts.DMS.Logic
{
    public class MasterLogic : IMasterLogic
    {
        private static MasterLogic _instance = null;

        public static IMasterLogic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MasterLogic();

                return _instance;
            }
        }

        private MasterLogic()
        {
        }
    }
}   
