using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransfeloTask.Common;
using TransfeloTask.Dto;
using TransfeloTask.Models;
using TransfeloTask.Services;

namespace TransfeloTask.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("{id}")]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult<Driver>> GetDriverById(int id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult<PagedList<Driver>>> GetDrivers([FromQuery] PagingParams paging)
        {
            var drivers = await _driverService.GetAllAsync(paging);
            return Ok(drivers);
        }

        [HttpPost]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> CreateDriver([FromBody] DriverDto driverDto)
        {
            if (ModelState.IsValid)
            {
                await _driverService.CreateDriverAsync(driverDto);
                return Ok();
            }
            else
                return BadRequest($"Modle Not Valid");
        }

        [HttpPut("{id}")]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> UpdateDriver(int id, [FromBody] DriverDto driverDto)
        {
            if ( id == 0)
            {
                return BadRequest("Id Must have a value");
            }
            await _driverService.UpdateDriverAsync(id, driverDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> DeleteDriver(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id Must have a value");
            }
            await _driverService.DeleteDriverAsync(id);
            return Ok();
        }



        [HttpPost]
        [Route("insert-random")]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> InsertRandomNames()
        {

                    await _driverService.InsertRandomNames();
            return Ok("Random names inserted successfully.");
        }


        [HttpGet]
        [Route("getalphabetizeddriverbyid/{id}")]

        public async Task<Microsoft.AspNetCore.Mvc.ActionResult<Driver>> GetalphabetizedDriverById([FromRoute]int id)
        {
            var driver = await _driverService.GetAlphabetizedDriverByIdAsync(id);
            return Ok(driver);
        }



    }
}
