using System.Web.Http;
using OPENSCSMModel.Exchange;

namespace OPENSCSMAccessM.Controllers
{
    public class PowerShellController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Execute([FromBody] EPackage request)
        {
            if (request == null || string.IsNullOrEmpty(request.ActionName))
                return BadRequest("Action Name es requerido");

            string result = PowerShellRunner.ExecuteEmbeddedScriptIncidents(request);

            return Ok(result);
        }

    [HttpPost]
        public IHttpActionResult ToolsExecute([FromBody] EPackage request)
        {
            if (request == null || string.IsNullOrEmpty(request.ActionName))
                return BadRequest("Action Name es requerido");

            string result = PowerShellRunner.ExecuteEmbeddedScriptTools(request);

            return Ok(result);
        }
    }
}