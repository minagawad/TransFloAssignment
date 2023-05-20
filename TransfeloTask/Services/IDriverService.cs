using TransfeloTask.Common;
using TransfeloTask.Dto;
using TransfeloTask.Models;

namespace TransfeloTask.Services
{
    public interface IDriverService
    {
        Task DeleteDriverAsync(int id);
        Task<PagedList<Driver>> GetAllAsync(PagingParams paging);
        Task<Driver> GetDriverByIdAsync(int id);
        Task UpdateDriverAsync(int id, DriverDto driver);
        Task CreateDriverAsync(DriverDto driver);
        Task InsertRandomNames();
        Task<string> GetAlphabetizedDriverByIdAsync(int id);
    }

}
