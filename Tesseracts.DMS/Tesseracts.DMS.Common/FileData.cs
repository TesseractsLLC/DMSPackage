using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesseracts.DMS.Common
{
    public class FileData
    {
        public FileData() { }
        public string Name { get; set; }
        public byte[] DataBuffer { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public string Value { get { return Encoding.Default.GetString(DataBuffer); } }
        public bool IsAFileUpload { get { return !String.IsNullOrEmpty(FileName); } }
    }
}
