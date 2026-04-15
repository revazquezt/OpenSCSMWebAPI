using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMModel;

namespace OPENSCSMAAPI.Controlers
{
    /// <summary>
    /// Add a file to an incident.
    /// </summary>
    /// <param name="request">The object that contains the incident data and resolution data.</param>
    /// <returns>Update Result.</returns>
    [ApiExplorerSettings(GroupName = "SCSM Incidents")]
    [Route("Incidents")]
    [ApiController]
    public class UpLoadController : ControllerBase
    {
        private readonly ISCSMService _scsmService;
        private readonly IConfigurationRoot ConfigRoot;
        private string scsmEnviroment = string.Empty;
        private string scsmDatabase = string.Empty;
        private readonly IWebHostEnvironment _env;

        public UpLoadController(IWebHostEnvironment env, IConfiguration configRoot, ISCSMService scsmService)
        {
            _env = env;
            ConfigRoot = (IConfigurationRoot)configRoot;
            scsmEnviroment = ConfigRoot["ScsmEnviroment"]!;
            scsmDatabase = ConfigRoot["ScsmDataBase"]!;
            _scsmService = scsmService;
        }

        [HttpPost]
        [Route("Addfile")]
        public async Task<IActionResult> UploadFile([FromForm] IdRequest request, IFormFile file)
        {
            if (request == null || string.IsNullOrEmpty(request.IdIncident))
            {
                return BadRequest("The Incident Request is Required.");
            }

            if (file == null || file.Length == 0)
                return BadRequest("Invalid File");

            if (file.Length >= 2621440)
                return BadRequest("Bad Size File");

            var uploadPath = System.IO.Path.Combine(_env.ContentRootPath, "Uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var uniqueName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
            var filePath = System.IO.Path.Combine(uploadPath, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var parameters = new Dictionary<string, object>
                            {
                                { "FilePath", filePath},
                                { "IRid",  request.IdIncident.ToString() },
                                { "Database", scsmDatabase },
                                { "scsmEnviroment", scsmEnviroment }
                            };

            string result = await _scsmService.EjecutarToolsScriptAsync(JsonConvert.SerializeObject(parameters), "Addfile");
            result = Regex.Unescape(result).Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}");
            
            return Ok(new
            {
                message = "File uploaded successfully",
                fileName = file.FileName,
                path = filePath,
                IdRequest = request.IdIncident
            });
        }

    }
}
