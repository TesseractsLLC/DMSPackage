using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Tesseracts.DMS.Common;
using Tesseracts.DMS.Logic;

namespace Tesseracts.DMS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DocumentController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                var formItems = new List<FileData>();
                foreach (HttpContent contentPart in provider.Contents)
                {
                    var formItem = new FileData();
                    var contentDisposition = contentPart.Headers.ContentDisposition;
                    formItem.Name = contentDisposition.Name.Trim('"');
                    formItem.DataBuffer = await contentPart.ReadAsByteArrayAsync();
                    formItem.FileName = String.IsNullOrEmpty(contentDisposition.FileName) ? "" : contentDisposition.FileName.Trim('"');
                    formItem.MediaType = contentPart.Headers.ContentType == null ? "" :
                        String.IsNullOrEmpty(contentPart.Headers.ContentType.MediaType) ? "" : contentPart.Headers.ContentType.MediaType;
                    formItems.Add(formItem);
                }
                DocumentLogic.Instance.SaveFile(formItems);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Content(HttpStatusCode.InternalServerError, ex.ToString());
            }

            return Ok();
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile(string fileId)
        {
            HttpResponseMessage response = null;
            try
            {
                var file = DocumentLogic.Instance.DownloadFile(fileId);
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(file.DataBuffer);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = file.FileName
                };

                response.Content.Headers.ContentDisposition.FileName = file.FileName;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return response;
        }
    }
}
