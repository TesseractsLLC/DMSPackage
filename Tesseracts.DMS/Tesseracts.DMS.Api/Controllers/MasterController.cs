using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Tesseracts.DMS.Common;
using Tesseracts.DMS.Logic;

namespace Tesseracts.DMS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MasterController : ApiController
    {
        [HttpGet]
        public string GetMyName()
        {
            return "Nixon Chakiath";
        }

        [HttpGet]
        [ActionName("GetAllDocumentTags")]
        public IEnumerable<DocumentTagDetails> GetAllDocumentTags()
        {
            return MasterLogic.Instance.GetAllDocumentTags();
        }

        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.Contents)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();
                    //Do whatever you want with filename and its binary data.

                    DocumentLogic.Instance.SaveFile(filename, buffer);
                }
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Content(HttpStatusCode.InternalServerError, ex.ToString());
            }

            return Ok();
        }

    }
}