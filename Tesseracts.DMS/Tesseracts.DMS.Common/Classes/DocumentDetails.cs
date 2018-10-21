using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesseracts.DMS.Common
{
    public class DocumentDetails : BaseEntity
    {
        public long Id { get; set; }
        public string UniqueFileName { get; set; }
        public string FileSavedPlatform { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long? DocumentTagType { get; set; }
        public string DocumentTagValue { get; set; }
        public bool? IsActive { get; set; }

        public virtual DocumentTagDetails DocumentTag { get; set; }
    }
}
