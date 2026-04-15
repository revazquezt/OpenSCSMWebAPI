
namespace OPENSCSMAAPI.WSCSMServiceAccess
{
    public interface ISCSMService
    {
        Task<string> EjecutarScriptAsync(string request, string actionName);
        Task<string> EjecutarToolsScriptAsync(string request, string actionName);
        Task<string> GetCatalogsItemsAsync(string catalogName);
        Task<string> GetSrCatalogsItemsAsync(string catalogName);
    }
}
