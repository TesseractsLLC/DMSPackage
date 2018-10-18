using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseracts.DMS.Common;
using Tesseracts.DMS.Data;

namespace Tesseracts.DMS.Logic
{
    public class DocumentLogic : IDocumentLogic
    {
        private static IDocumentLogic _instance = null;

        private string _documentSaveFolder;

        public static IDocumentLogic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DocumentLogic();

                return _instance;
            }
        }

        private DocumentLogic()
        {

        }

        /// <summary>
        /// Save the uploaded file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public bool SaveFile(string fileName, byte[] fileData)
        {
            var status = false;
            try
            {
                var documentFolder = DatabaseHelper.DocumentFolder;
                if (!Directory.Exists(documentFolder))
                {
                    Directory.CreateDirectory(documentFolder);
                }

                var uniqueFileName = string.Format("{0}\\{1}_{2}", documentFolder, Guid.NewGuid().ToString(), fileName);
                File.WriteAllBytes(uniqueFileName, fileData);

                status = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
            return status;
        }
    }
}
