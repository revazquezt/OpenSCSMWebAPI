using System.Text.Json;
using OPENSCSMAAPI.WSCSMServiceAccess;
using OPENSCSMModel;

namespace OPENSCSMAPI.GraphQL
{
    public class CatalogssrItemsQuery
    {
        private readonly ISCSMService _scsmService;

        public CatalogssrItemsQuery(ISCSMService scsmService)
        {
            _scsmService = scsmService;
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum>> GetServiceRequestAreaEnum()
        {
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Content>> GetServiceRequestAreaEnum_Content()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Content);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Content>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Directory>> GetServiceRequestAreaEnum_Directory()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Directory);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Directory>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Facilities>> GetServiceRequestAreaEnum_Facilities()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Facilities);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Facilities>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_File>> GetServiceRequestAreaEnum_File()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_File);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_File>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Hardware>> GetServiceRequestAreaEnum_Hardware()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Hardware);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Hardware>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Infrastructure>> GetServiceRequestAreaEnum_Infrastructure()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Infrastructure);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Infrastructure>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Messaging>> GetServiceRequestAreaEnum_Messaging()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Messaging);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Messaging>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Operations>> GetServiceRequestAreaEnum_Operations()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Operations);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Operations>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Other>> GetServiceRequestAreaEnum_Other()  //Revisar si es de otro catalogo o esta erroneo el nombre, no devuelve registros
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Other);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Other>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Security>> GetServiceRequestAreaEnum_Security()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Security);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Security>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestAreaEnum_Software>> GetServiceRequestAreaEnum_Software()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestAreaEnum_Software);
            return JsonSerializer.Deserialize<List<ServiceRequestAreaEnum_Software>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestImplementationResultsEnum>> GetServiceRequestImplementationResultsEnum()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestImplementationResultsEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestImplementationResultsEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestPriorityEnum>> GetServiceRequestPriorityEnum()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestPriorityEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestPriorityEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestSourceEnum>>GetServiceRequestSourceEnum()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestSourceEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestSourceEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestStatusEnum>> GetServiceRequestStatusEnum()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestStatusEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestStatusEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestSupportGroupEnum>> GetServiceRequestSupportGroupEnum() //Revisar si es de otro catalogo o esta erroneo el nombre, no devuelve registros
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestSupportGroupEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestSupportGroupEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }

        [UseFiltering]
        public async Task<IEnumerable<ServiceRequestUrgencyEnum>> GetServiceRequestUrgencyEnum()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string json = await _scsmService.GetSrCatalogsItemsAsync(Const.ServiceRequestUrgencyEnum);
            return JsonSerializer.Deserialize<List<ServiceRequestUrgencyEnum>>(json.Replace("\\\"", "\"").Replace("\\r\\n", "").Replace("\"[", "[").Replace("]\"", "]"), options)!;
            
        }
    }
}
