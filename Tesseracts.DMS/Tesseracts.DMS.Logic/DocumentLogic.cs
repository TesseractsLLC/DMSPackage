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
        /// Download the saved file
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public FileData DownloadFile(string fileId)
        {
            FileData fileData = null;
            try
            {
                using (var db = new Entities(DatabaseHelper.ConnectionString))
                {
                    var document = db.Documents.FirstOrDefault(doc => doc.UniqueFileName == fileId);
                    if (document != null)
                    {
                        var documentFolder = DatabaseHelper.DocumentFolder;
                        var fileName = string.Format("{0}\\{1}", documentFolder, document.UniqueFileName);
                        var fileBuffer = File.ReadAllBytes(fileName);

                        fileData = new FileData
                        {
                            FileName = document.Name,
                            DataBuffer = fileBuffer
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
            return fileData;
        }

        /// <summary>
        /// Save the file in the server
        /// </summary>
        /// <param name="fileUploadData"></param>
        /// <returns></returns>
        public bool SaveFile(List<FileData> fileUploadData)
        {
            try
            {
                using (var db = new Entities(DatabaseHelper.ConnectionString))
                {
                    var parameters = fileUploadData.Where(x => !x.IsAFileUpload);
                    IDictionary<int, string> documentTagValues = new Dictionary<int, string>();
                    var documentTag = 0;
                    var documentTagValue = string.Empty;
                    foreach (var param in parameters)
                    {
                        if (param.Name == "DocumentTag")
                        {
                            documentTag = Convert.ToInt32(param.Value);
                        }

                        if (param.Name == "DocumentTagValue")
                        {
                            documentTagValue = param.Value;
                        }
                    }
                    documentTagValues[documentTag] = documentTagValue;

                    var filesToUpload = fileUploadData.Where(x => x.IsAFileUpload);
                    foreach (FileData file in filesToUpload)
                    {
                        var uniqueFileName = SaveFile(file.FileName, file.DataBuffer);
                        foreach (KeyValuePair<int, string> tag in documentTagValues)
                        {
                            var currentDoc = new Document
                            {
                                UniqueFileName = uniqueFileName,
                                Name = file.FileName,
                                Extension = Path.GetExtension(file.FileName),
                                DocumentTagType = tag.Key,
                                DocumentTagValue = tag.Value,
                                CreatedOn = DateTime.UtcNow,
                                CreatedBy = "System",
                                IsActive = true
                            };
                            db.Documents.Add(currentDoc);
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
            return true;
        }

        /// <summary>
        /// Save the uploaded file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        private string SaveFile(string fileName, byte[] fileData)
        {
            var uniqueFileName = string.Empty;
            try
            {
                var documentFolder = DatabaseHelper.DocumentFolder;
                if (!Directory.Exists(documentFolder))
                {
                    Directory.CreateDirectory(documentFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString();
                var fullFilePath = string.Format("{0}\\{1}", documentFolder, uniqueFileName);
                File.WriteAllBytes(fullFilePath, fileData);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
            return uniqueFileName;
        }
    }
}
