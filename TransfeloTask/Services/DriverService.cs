using System.Security.AccessControl;
using TransfeloTask.Common;
using TransfeloTask.Dto;
using TransfeloTask.Models;
using TransfeloTask.Repositories;

namespace TransfeloTask.Services
{

    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task DeleteDriverAsync(int id)
        {
            await _driverRepository.DeleteDriverAsync(id);
        }

        public async Task<PagedList<Driver>> GetAllAsync(PagingParams paging)
        {
            return await _driverRepository.GetAllAsync(paging);
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            return await _driverRepository.GetDriverByIdAsync(id);
        }

        public async Task<string> GetAlphabetizedDriverByIdAsync(int id)
        {
            string alphabetizedName = string.Empty;
            var driver = await _driverRepository.GetDriverByIdAsync(id);
            if (driver != null &&! string.IsNullOrEmpty( driver.FirstName)&&! string.IsNullOrEmpty( driver.LastName))
            {
                alphabetizedName = string.Concat(driver.FirstName," ", driver.LastName)
                 .OrderBy(c => c)
                 .ToString();

            }

            return alphabetizedName;

        }

        public async Task UpdateDriverAsync(int id, DriverDto driver)
        {
            await _driverRepository.UpdateDriverAsync(id, driver);
        }

        public async Task CreateDriverAsync(DriverDto driver)
        {
            await _driverRepository.CreateDriverAsync(driver);
        }

        public async Task InsertRandomNames()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            List<DriverDto> drivers = new List<DriverDto>();
            for (int i = 0; i < 100; i++)
            {
                string firstName = new string(Enumerable.Repeat(chars, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                string lastName = new string(Enumerable.Repeat(chars, 7)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                var driver = new DriverDto
                {

                    FirstName = firstName,
                    LastName = lastName,
                    Email = $"{firstName}.{lastName}@example.com",
                    PhoneNumber = GenerateRandomPhoneNumber()
                };

                drivers.Add(driver);
            }

            await _driverRepository.BulkInsertAsync(drivers);
        }

        private string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            const string digits = "0123456789";
            return new string(Enumerable.Repeat(digits, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
