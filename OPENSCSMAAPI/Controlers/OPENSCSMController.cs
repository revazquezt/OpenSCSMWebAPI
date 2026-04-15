using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMModel;

namespace OPENSCSMAPI.Controlers
{
    [ApiExplorerSettings(GroupName = "SCSM Incidents")]
    [Route("Incidents")]
    [ApiController]
    public class OPENSCSMController : ControllerBase
    {
        private readonly ISCSMService _scsmService;

        public OPENSCSMController(ISCSMService scsmService) {
            _scsmService = scsmService;
        }

        /// <summary>
        /// View incidents by affected user and status.
        /// </summary>
        /// <param name="request">The object that contains the user's ID.</param>
        /// <returns>List of incidents related to the affected user.</returns>
        [HttpPost("GetIncidentsByUserAfected")]
        public async Task<IActionResult> Post([FromBody] ConsultByUserAfected request)
        {
            if (request == null || string.IsNullOrEmpty(request.PrincipalUserName))
            {
                return BadRequest("The Principal User Name is Required.");
            }

            string result = await _scsmService.EjecutarScriptAsync(JsonConvert.SerializeObject(request), "ConsultByUserAfected");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]");
            return Ok(result);
        }

        /// <summary>
        /// View incidents by assigned user and status.
        /// </summary>
        /// <param name="request">The object that contains the user's ID.</param>
        /// <returns>List of incidents related to the assigned user.</returns>
        [HttpPost("GetIncidentsByUserAssigned")]
        public async Task<IActionResult> Post([FromBody] ConsultByUserAssigned request)
        {
            if (request == null || string.IsNullOrEmpty(request.PrincipalUserName))
            {
                return BadRequest("The Principal User Name is Required.");
            }

            string result = await _scsmService.EjecutarScriptAsync(JsonConvert.SerializeObject(request), "ConsultByUserAssigned");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]");
            return Ok(result);
        }

        /// <summary>
        /// View incidents by ID.
        /// </summary>
        /// <param name="request">The object that contains the Incident ID to be Queryed.</param>
        /// <returns>List of incident properties.</returns>
        [HttpPost("GetIncidentById")]
        public async Task<IActionResult> ByID([FromBody] IdRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.IdIncident))
            {
                return BadRequest("The Incident ID is required.");
            }

            string result = await _scsmService.EjecutarScriptAsync(JsonConvert.SerializeObject(request), "GetIncidentById");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]");
            return Ok(result);

        }

        /// <summary>
        /// Register a new incident.
        /// </summary>
        /// <param name="request">The object that contains the data of the incident to be recorded.</param>
        /// <returns>Incident ID .</returns>
        [HttpPost("NewIncident")]
        public async Task<IActionResult> Post([FromBody] NewIncident request)
        {
            if (request == null ||
                string.IsNullOrEmpty(request.Id) ||
                string.IsNullOrEmpty(request.Title) ||
                string.IsNullOrEmpty(request.Description) ||
                string.IsNullOrEmpty(request.Urgency) ||
                string.IsNullOrEmpty(request.Impact) ||
                string.IsNullOrEmpty(request.Source) ||
                string.IsNullOrEmpty(request.Status) ||
                string.IsNullOrEmpty(request.Classification) ||
                string.IsNullOrEmpty(request.TierQueue)

                )
            {
                return BadRequest("Data of new Incident is Incompleted.");
            }

            string result = await _scsmService.EjecutarScriptAsync(JsonConvert.SerializeObject(request), "NewIncident");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]");
            return Ok(result);

        }

        /// <summary>
        /// Resolution of an incident.
        /// </summary>
        /// <param name="request">The object that contains the incident data and resolution data.</param>
        /// <returns>Update Result .</returns>
        [HttpPost("ResolveIncident")]
        public async Task<IActionResult> Post([FromBody] ResolveIncident request)
        {
            if (request == null ||
                string.IsNullOrEmpty(request.incidentID) ||
                string.IsNullOrEmpty(request.ResolvedUserPrincipalName) ||
                string.IsNullOrEmpty(request.IncidentResolutionDescription) ||
                string.IsNullOrEmpty(request.IncidentresolutionCategory) 

                )
            {
                return BadRequest("Data for Resolution is nolt completed.");
            }

            string result = await _scsmService.EjecutarScriptAsync(JsonConvert.SerializeObject(request), "ResolveIncident");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]");
            return Ok(result);

        }

        /// <summary>
        /// Add comments to an incident.
        /// </summary>
        /// <param name="request">The object containing the incident data and comments.</param>
        /// <returns>Update Result.</returns>
        [HttpPost("AddAnalisysComment2Incident")]
        public async Task<IActionResult> Post([FromBody] AddAnalisysCommentIncident request)
        {
            if (request == null ||
                string.IsNullOrEmpty(request.incidentID) ||
                string.IsNullOrEmpty(request.IncidentcommentText) ||
                string.IsNullOrEmpty(request.IncidentCommentAddedBy) ||
                string.IsNullOrEmpty(request.IncidentisPrivateComment)

                )
            {
                return BadRequest("Data for Resolution is nolt completed.");
            }

            string result = await _scsmService.EjecutarScriptAsync(JsonConvert.SerializeObject(request), "AddAnalisysComment2Incident");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]");
            return Ok(result);

        }        
    }
}
