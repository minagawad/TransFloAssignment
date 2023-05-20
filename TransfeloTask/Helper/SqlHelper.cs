using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace TransfeloTask.Helper
{
    public class SqlHelper
    {
        private readonly IConfiguration _configuration;

        public SqlHelper(
                IConfiguration configuration )
        {
            _configuration = configuration;
        }


        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<SqlConnection> GetConnectionAsync()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            return connection;
        }

        private SqlCommand GetCommand(
            SqlConnection connection,
            string commandText,
            CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection)
            {
                CommandType = commandType
            };

            return command;
        }

        public SqlParameter NewParameter(
            string parameterName,
            object parameterValue,
            ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter parameterObject = new SqlParameter(parameterName, parameterValue ?? DBNull.Value)
            {
                Direction = direction
            };

            return parameterObject;
        }

        public async Task<int> ExecuteNonQueryAsync(
            string procedureName,
            List<SqlParameter> parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            using (SqlConnection connection = await this.GetConnectionAsync())
            {
                SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);

                if (parameters != null && parameters.Count > 0)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }

                returnValue = await cmd.ExecuteNonQueryAsync();
            }

            return returnValue;
        }

        public async Task<object> ExecuteScalarAsync(
            string procedureName,
            List<SqlParameter> parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            object returnValue = null;

            using (SqlConnection connection = await this.GetConnectionAsync())
            {
                SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);

                if (parameters != null && parameters.Count > 0)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }

                returnValue = await cmd.ExecuteScalarAsync();
            }

            return returnValue;
        }

        public async Task<SqlDataReader> ExcuteDataReaderAsync(
            SqlConnection connection,
            string procedureName,
            List<SqlParameter> parameters = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            SqlDataReader ds;

            SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);
            if (parameters != null && parameters.Count > 0)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            ds = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            return ds;
        }

        public List<T> DataReaderMapToList<T>(
            IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj;

            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null/* TODO Change to default(_) if this is not a reference type */);
                    }
                }
                list.Add(obj);
            }

            return list;
        }
    }

}
