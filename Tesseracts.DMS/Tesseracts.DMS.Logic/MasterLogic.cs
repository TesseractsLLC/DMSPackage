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

        public List<DocumentTagDetails> GetAllDocumentTags()
        {
            List<DocumentTagDetails> documentTags = new List<DocumentTagDetails>();
            using (var db = new Entities(DatabaseHelper.ConnectionString))
            {
                foreach (var tag in db.DocumentTags)
                {
                    documentTags.Add(new DocumentTagDetails
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        CreatedAt = tag.CreatedAt,
                        CreatedBy = tag.CreatedBy
                    });
                }
            }

            return documentTags.ToList();
        }
    }
}   
