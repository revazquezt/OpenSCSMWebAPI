using System.Text.Json;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMModel;

namespace OPENSCSMAPI.GraphQL
{
    public class CatalogsItemsQuery
    {
        private readonly ISCSMService _scsmService;

        public CatalogsItemsQuery(ISCSMService scsmService)
        {
            _scsmService = scsmService;
        }

        [UseFiltering]
        public async Task<IEnumerable<TroubleTicketUrgency>> GetTroubleTicketUrgency() 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string json = await _scsmService.GetCatalogsItemsAsync(Const.TroubleTicketUrgency.ToString());
            return JsonSerializer.Deserialize<List<TroubleTicketUrgency>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
        [UseFiltering]
        public async Task<IEnumerable<TroubleTicketImpact>> GetTroubleTicketImpact() 
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetCatalogsItemsAsync(Const.TroubleTicketUrgency);
            return JsonSerializer.Deserialize<List<TroubleTicketImpact>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
        [UseFiltering]
        public async Task<IEnumerable<IncidentSource>> GetIncidentSource() 
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetCatalogsItemsAsync(Const.IncidentSource);
            return JsonSerializer.Deserialize<List<IncidentSource>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
        [UseFiltering]
        public async Task<IEnumerable<IncidentStatus>> GetIncidentStatus()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetCatalogsItemsAsync(Const.IncidentStatus);
            return JsonSerializer.Deserialize<List<IncidentStatus>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
        [UseFiltering]
        public async Task<IEnumerable<IncidentClassification>> GetIncidentClassification()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetCatalogsItemsAsync(Const.IncidentClassification);
            return JsonSerializer.Deserialize<List<IncidentClassification>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
        [UseFiltering]
        public async Task<IEnumerable<IncidentTier>> GetIncidentTier()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetCatalogsItemsAsync(Const.IncidentTier);
            return JsonSerializer.Deserialize<List<IncidentTier>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
    }
}