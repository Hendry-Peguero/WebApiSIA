using System.Data;

namespace WebApiSIA.Core.Application.Interfaces.Helpers
{
    public interface ISqlHelper
    {
        DataTable ExecuteSQLStoredProcedure(string storedProcName, Dictionary<string, object> parameters = null);
    }
}
