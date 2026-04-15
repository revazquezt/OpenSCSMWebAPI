
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMModel.UsersProfiles;

namespace OPENSCSMAPI.Controlers
{
    [ApiExplorerSettings(GroupName = "SCSM Users")]
    [Route("Users")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly ISCSMService _scsmService;
        public UserProfilesController(IConfiguration configRoot, ISCSMService scsmService, ILogger<OPENSCSMController> logger)
        {
            _scsmService = scsmService;
        }

        /// <summary>
        /// Check if the user is registered and their status.
        /// </summary>
        /// <param name="request">The object that contains the user.</param>
        /// <returns>The first and last name, the user identifier, and the user status or a null value.</returns>
        [HttpPost("GetUserStatus")]
        public async Task<IActionResult> Post([FromBody] ConsultUserStatus request)
        {
            if (request == null || string.IsNullOrEmpty(request.PrincipalUserName))
            {
                return BadRequest("The Principal User Name is Required.");
            }

            string result = await _scsmService.EjecutarToolsScriptAsync(JsonConvert.SerializeObject(request), "GetUserStatus");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}");
            return Ok(result);
        }
    }
}
