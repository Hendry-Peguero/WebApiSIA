using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Application.Interfaces.Helpers;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Helpers
{
    public class SqlHelper : ISqlHelper
    {
        private readonly ApplicationContext _context;

        public SqlHelper(ApplicationContext context)
        {
            _context = context;
        }

        public DataTable ExecuteSQLStoredProcedure(string storedProcName, Dictionary<string, object> parameters = null)
        {
            var dt = new DataTable();

            // Usamos la conexión que maneja EF (ApplicationContext)
            var connection = _context.Database.GetDbConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = storedProcName;
                    command.CommandType = CommandType.StoredProcedure;

                    // Añadir los parámetros si se enviaron
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            var dbParam = command.CreateParameter();
                            dbParam.ParameterName = param.Key;
                            dbParam.Value = param.Value ?? DBNull.Value;
                            command.Parameters.Add(dbParam);
                        }
                    }

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return dt;
        }
    }
}
