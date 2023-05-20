using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using TransfeloTask.Common;
using TransfeloTask.Dto;
using TransfeloTask.Helper;
using TransfeloTask.Models;

namespace TransfeloTask.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly SqlHelper _sqlHelper;

        public DriverRepository(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper ?? throw new ArgumentNullException(nameof(sqlHelper));
        }




        public async Task DeleteDriverAsync(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                this._sqlHelper.NewParameter("@Id", id),

            };

            await this._sqlHelper.ExecuteScalarAsync("[dr].[usp_DeleteDriver]", parameters);
        }

        public async Task<PagedList<Driver>> GetAllAsync(PagingParams paging)
        {
            int totalCount = 0;
            int page = 0;
            int pageSize = 0;
            var result = new List<Driver>();

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                this._sqlHelper.NewParameter("@PageNumber", paging.PageNumber),
                this._sqlHelper.NewParameter("@PageSize", paging.PageSize),

            };

            using (SqlConnection connection = await this._sqlHelper.GetConnectionAsync())
            {
                var dr = await this._sqlHelper.ExcuteDataReaderAsync(connection, "[dr].[usp_GetDriversPaged]", parameters);

                while (await dr.ReadAsync())
                {
                    totalCount = (int)dr["TotalCount"];
                    page = (int)dr["Page"];
                    pageSize = (int)dr["PageSize"];
                    result.Add(Map(dr));
                }
            }

            return new PagedList<Driver>()
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                List = result,
            };
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            Driver driver = null;
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                this._sqlHelper.NewParameter("@Id", id),

            };

            using (SqlConnection connection = await this._sqlHelper.GetConnectionAsync())
            {
                var dr = await this._sqlHelper.ExcuteDataReaderAsync(connection, "[dr].[usp_GetDriverById]", parameters);

                while (dr.Read())
                {
                    driver = new Driver()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = (string) dr["FirstName"],
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString()
                    };


                }

                dr.Close();
            }

            return driver;
        }

        public async Task UpdateDriverAsync(int id, DriverDto driver)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                this._sqlHelper.NewParameter("@Id", id),
                this._sqlHelper.NewParameter("@FirstName", driver.FirstName),
                this._sqlHelper.NewParameter("@LastName", driver.LastName),
                this._sqlHelper.NewParameter("@Email", driver.Email),
                this._sqlHelper.NewParameter("@PhoneNumber", driver.PhoneNumber)
            };

            await this._sqlHelper.ExecuteScalarAsync("[dr].[usp_UpdateDriver]", parameters);
        }

        public async Task CreateDriverAsync(DriverDto driver)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                this._sqlHelper.NewParameter("@FirstName", driver.FirstName),
                this._sqlHelper.NewParameter("@LastName", driver.LastName),
                this._sqlHelper.NewParameter("@Email", driver.Email),
                this._sqlHelper.NewParameter("@PhoneNumber", driver.PhoneNumber)
            };

            await this._sqlHelper.ExecuteScalarAsync("[dr].[usp_CreatDriver]", parameters);
        }

        private Driver Map(SqlDataReader dr)
        {
            return new Driver
            {
                Id = (int)dr["Id"],
                FirstName = (string)dr["FirstName"],
                LastName = (string)dr["LastName"],
                Email = (string)dr["Email"],
                PhoneNumber = (string)dr["PhoneNumber"]

            };

        }

        public async Task BulkInsertAsync(List<DriverDto> drivers)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            if (drivers.Any())
            {
                List<SqlDataRecord> newdriverDatatable = new List<SqlDataRecord>();
                var driverTemp = new SqlMetaData[]
                {
                    new SqlMetaData("FirstName", System.Data.SqlDbType.NVarChar,50),
                    new SqlMetaData("LastName", System.Data.SqlDbType.NVarChar,50),
                    new SqlMetaData("Email", System.Data.SqlDbType.NVarChar,50),
                    new SqlMetaData("PhoneNumber", System.Data.SqlDbType.NVarChar,50),

                };

                foreach (var driver in drivers)
                {
                    SqlDataRecord row = new SqlDataRecord(driverTemp);
                    row.SetValues(driver.FirstName, driver.LastName, driver.Email, driver.PhoneNumber);
                    newdriverDatatable.Add(row);
                }

                var newFieldsPara = this._sqlHelper.NewParameter("@DriverList", newdriverDatatable);

                newFieldsPara.SqlDbType = System.Data.SqlDbType.Structured;
                newFieldsPara.TypeName = "[dr].[DriversListType]";

                parameters.Add(newFieldsPara);
            }
            await this._sqlHelper.ExecuteScalarAsync("[dr].[usp_BulkInsertDrivers]", parameters);

        }
    }
}
