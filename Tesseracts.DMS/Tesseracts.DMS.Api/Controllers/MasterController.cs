using System.Collections.Generic;
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

    }
}