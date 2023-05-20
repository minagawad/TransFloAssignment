using TransfeloTask.Common;
using TransfeloTask.Dto;
using TransfeloTask.Models;

namespace TransfeloTask.Repositories
{
    public interface IDriverRepository
    {
        Task<PagedList<Driver>> GetAllAsync(PagingParams paging);
        Task<Driver> GetDriverByIdAsync(int id);
        Task CreateDriverAsync(DriverDto driver);
        Task UpdateDriverAsync(int id, DriverDto driver);
        Task DeleteDriverAsync(int id);

        Task BulkInsertAsync(List<DriverDto> drivers);
    }
}
