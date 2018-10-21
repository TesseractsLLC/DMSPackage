using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesseracts.DMS.Common
{
    public interface IDocumentLogic
    {
        IEnumerable<DocumentTagDetails> GetAllDocumentTags();

        IEnumerable<DocumentDetails> GetAllDocuments();

        IEnumerable<DocumentDetails> GetAllDocuments(int documentTagId, string documentTagValue);

        bool UploadFile(List<FileData> fileUploadData);

        FileData DownloadFile(string fileId);
    }
}
