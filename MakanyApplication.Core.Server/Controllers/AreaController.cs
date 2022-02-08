using MakanyApplication.Shared.Models.DataTransferObjects.Area;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly AreaRepo _areaRepo;
        public AreaController(AreaRepo areaRepo)
        {
            _areaRepo = areaRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateArea model)
            => Ok(await _areaRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateArea model)
            => Ok(await _areaRepo.UpdateAsync(model));

        [HttpGet("GetAreaForUpdate/{Id:int}")]
        public async Task<IActionResult> GetAreaForUpdate(int Id)
            => Ok(await _areaRepo.GetAreaForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _areaRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _areaRepo.GetAsync());
    }
}
