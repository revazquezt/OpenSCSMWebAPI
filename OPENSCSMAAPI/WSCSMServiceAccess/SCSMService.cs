using Newtonsoft.Json;
using System.Text;

namespace OPENSCSMAAPI.WSCSMServiceAccess
{

    public class SCSMService : ISCSMService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<SCSMService> _logger;
        private readonly IConfigurationRoot ConfigRoot;

        public SCSMService(IHttpClientFactory clientFactory, ILogger<SCSMService> logger, IConfiguration configRoot)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            ConfigRoot = (IConfigurationRoot)configRoot;
            }

        public async Task<string> EjecutarScriptAsync(
            string request,
            string actionName)
        {
            try
            {
                var client = _clientFactory.CreateClient("PowerShellWorker");

                var payload = new { ActionName = actionName, Request = request };
                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Ejecutando script: {ActionName}", actionName);

                var response = await client.PostAsync(ConfigRoot["WorkerUrlExec"]!, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error conectando al PowerShell Worker");
                throw new Exception("No se pudo conectar al servicio PowerShell", ex);
            }
        }

        public async Task<string> EjecutarToolsScriptAsync(
            string request,
            string actionName)
        {
            try
            {
                var client = _clientFactory.CreateClient("PowerShellWorker");

                var payload = new { ActionName = actionName, Request = request };
                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Ejecutando script: {ActionName}", actionName);

                var response = await client.PostAsync(ConfigRoot["WorkerUrlToolsExec"]!, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error conectando al PowerShell Worker");
                throw new Exception("No se pudo conectar al servicio PowerShell", ex);
            }
        }

        public async Task<string> GetCatalogsItemsAsync(string catalogName)
        {
            var client = _clientFactory.CreateClient("PowerShellWorker");
            var response = await client.GetAsync($"{ConfigRoot["WorkerUrlCatalogs"]!}?catalogName={catalogName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSrCatalogsItemsAsync(string catalogName)
        {
            var client = _clientFactory.CreateClient("PowerShellWorker");
            var response = await client.GetAsync($"{ConfigRoot["WorkerUrlSrCatalogs"]!}?catalogName={catalogName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
