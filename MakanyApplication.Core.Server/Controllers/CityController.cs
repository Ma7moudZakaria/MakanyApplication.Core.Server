using MakanyApplication.Shared.Models.DataTransferObjects.City;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly CityRepo _cityRepo;
        public CityController(CityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CreateCity model)
            => Ok(await _cityRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCity model)
            => Ok(await _cityRepo.UpdateAsync(model));

        [HttpGet("GetCityForUpdate/{Id:int}")]
        public async Task<IActionResult> GetCityForUpdate(int Id)
            => Ok(await _cityRepo.GetCityForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _cityRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _cityRepo.GetAsync());
    }
}
