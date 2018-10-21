using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// Get all document tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocumentTagDetails> GetAllDocumentTags()
        {
            IEnumerable<DocumentTagDetails> documentTags = null;
            try
            {
                using (var db = new Entities(DatabaseHelper.ConnectionString))
                {
                    if (db.DocumentTags != null)
                    {
                        documentTags = db.DocumentTags.Select(entity => ConvertEntityToDocumentTag(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }

            return documentTags;
        }

        /// <summary>
        /// Get all the documents
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocumentDetails> GetAllDocuments()
        {
            IEnumerable<DocumentDetails> docs = null;
            try
            {
                using (var db = new Entities(DatabaseHelper.ConnectionString))
                {
                    if (db.Documents != null)
                    {
                        docs = db.Documents.Select(entity => ConvertEnityToDocument(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
            return docs;
        }

        /// <summary>
        /// Get documents with specified document tag and tag value
        /// </summary>
        /// <param name="documentTagId"></param>
        /// <param name="documentTagValue"></param>
        /// <returns></returns>
        public IEnumerable<DocumentDetails> GetAllDocuments(int documentTagId, string documentTagValue)
        {
            IEnumerable<DocumentDetails> docs = null;
            try
            {
                using (var db = new Entities(DatabaseHelper.ConnectionString))
                {
                    if (db.Documents != null)
                    {
                        docs = db.Documents.Where(doc => doc.DocumentTagType == documentTagId &&
                            doc.DocumentTagValue == documentTagValue).Select(entity => ConvertEnityToDocument(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
            return docs;
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
                throw;
            }
            return fileData;
        }

        /// <summary>
        /// Save the file in the server
        /// </summary>
        /// <param name="fileUploadData"></param>
        /// <returns></returns>
        public bool UploadFile(List<FileData> fileUploadData)
        {
            try
            {
                using (var db = new Entities(DatabaseHelper.ConnectionString))
                {
                    var parameters = fileUploadData.Where(x => !x.IsAFileUpload);
                    IDictionary<int, string> documentTagValues = GetDocumentTagParams(parameters);

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

        #region Private Methods

        /// <summary>
        /// Get the parameters passed as part file upload
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IDictionary<int, string> GetDocumentTagParams(IEnumerable<FileData> parameters)
        {
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
            return documentTagValues;
        }

        /// <summary>
        /// Save the uploaded file to the file system/cloud
        /// </summary>
        /// <param name="fileName">File name to be saved</param>
        /// <param name="fileData">File data buffer</param>
        /// <returns></returns>
        private string SaveFile(string fileName, byte[] fileData)
        {
            var uniqueFileId = string.Empty;
            try
            {
                var documentFolder = DatabaseHelper.DocumentFolder;
                if (!Directory.Exists(documentFolder))
                {
                    Directory.CreateDirectory(documentFolder);
                }

                uniqueFileId = Guid.NewGuid().ToString();
                var fullFilePath = string.Format("{0}\\{1}", documentFolder, uniqueFileId);
                File.WriteAllBytes(fullFilePath, fileData);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
            return uniqueFileId;
        }
        #endregion Private Methods

        #region Convertion Methods
        /// <summary>
        /// Convert document tag entity to DocumentTagDetails
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private DocumentTagDetails ConvertEntityToDocumentTag(DocumentTag tag)
        {
            return new DocumentTagDetails
            {
                Id = tag.Id,
                Name = tag.Name,
                CreatedOn = tag.CreatedAt,
                CreatedBy = tag.CreatedBy
            };
        }

        /// <summary>
        /// Convert Document db entity to DocumentDetails
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private DocumentDetails ConvertEnityToDocument(Document entity)
        {
            return new DocumentDetails
            {
                Id = entity.Id,
                UniqueFileName = entity.UniqueFileName,
                FileSavedPlatform = entity.FileSavedPlatform,
                Name = entity.Name,
                Extension = entity.Extension,
                DocumentTagType = entity.DocumentTagType,
                DocumentTagValue = entity.DocumentTagValue,
                DocumentTag = ConvertEntityToDocumentTag(entity.DocumentTag),
                IsActive = entity.IsActive,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy
            };
        }
        #endregion Convertion Methods
    }
}
