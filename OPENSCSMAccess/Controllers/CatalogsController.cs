using System.Web.Http;

namespace OPENSCSMAccessM.Controllers
{
    public class CatalogsController : ApiController
    {
        [HttpGet]
        [Route("api/catalogs/items")]
        public IHttpActionResult GetCatalogsItems(string catalogName)
        {
            if (string.IsNullOrEmpty(catalogName))
                return BadRequest("Catalog name is required");

            try
            {
                var result = PowerShellRunner.ExecuteEmbeddedScriptCat(catalogName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
