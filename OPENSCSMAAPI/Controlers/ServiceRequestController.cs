
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMModel.ServiceRequest;

namespace OPENSCSMAPI.Controlers
{
    [ApiExplorerSettings(GroupName = "SCSM Service Request")]
    [Route("ServiceRequest")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly ISCSMService _scsmService;

        public ServiceRequestController( ISCSMService scsmService)
        {
            _scsmService = scsmService;
        }

        /// <summary>
        /// Register a new Service Request.
        /// </summary>
        /// <param name="request">The object that contains the data of the Service Request to be registered.</param>
        /// <returns>The SR ID.</returns>
        [HttpPost("NewServiceRequest")]
        public async Task<IActionResult> Post([FromBody] NewServiceRequest request)
        {
            if (request == null ||
                string.IsNullOrEmpty(request.Id) ||
                string.IsNullOrEmpty(request.Title) ||
                string.IsNullOrEmpty(request.Description) ||
                string.IsNullOrEmpty(request.Urgency) ||
                string.IsNullOrEmpty(request.Priority) ||
                string.IsNullOrEmpty(request.Area) ||
                string.IsNullOrEmpty(request.SupportGroup) ||
                string.IsNullOrEmpty(request.AffectedUser) ||
                string.IsNullOrEmpty(request.Status) 
                )
            {
                return BadRequest("Data of new Incident is Incompleted.");
            }

            string result = await _scsmService.EjecutarToolsScriptAsync(JsonConvert.SerializeObject(request), "NewServiceRequest");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}");
            return Ok(result);
        }
    }
}
