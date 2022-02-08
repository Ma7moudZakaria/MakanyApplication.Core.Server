using MakanyApplication.Shared.Models.DataTransferObjects.Country;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepo _countryRepo;
        public CountryController(CountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountry model)
            => Ok(await _countryRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountry model)
            => Ok(await _countryRepo.UpdateAsync(model));

        [HttpGet("GetCountryForUpdate/{Id:int}")]
        public async Task<IActionResult> GetCountryForUpdate(int Id)
            => Ok(await _countryRepo.GetCountryForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _countryRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _countryRepo.GetAsync());
    }
}
