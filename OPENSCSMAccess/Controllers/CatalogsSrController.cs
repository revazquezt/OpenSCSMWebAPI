using System.Web.Http;

namespace OPENSCSMAccessM.Controllers
{
    public class CatalogsSrController : ApiController
    {
        [HttpGet]
        [Route("api/catalogs/sr/items")]
        public IHttpActionResult GetCatalogsSrItems(string catalogName)
        {
            if (string.IsNullOrEmpty(catalogName))
                return BadRequest("Catalog name is required");

            try
            {
                var result = PowerShellRunner.ExecuteEmbeddedScriptSrCat(catalogName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
