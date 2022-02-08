using MakanyApplication.Shared.Models.DataTransferObjects.Address;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly AddressRepo _addressRepo;
        public AddressController(AddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddress model)
            => Ok(await _addressRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddress model)
            => Ok(await _addressRepo.UpdateAsync(model));

        [HttpGet("GetAddressForUpdate/{Id:int}")]
        public async Task<IActionResult> GetAddressForUpdate(int Id)
            => Ok(await _addressRepo.GetAddressForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _addressRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _addressRepo.GetAsync());
    }
}
