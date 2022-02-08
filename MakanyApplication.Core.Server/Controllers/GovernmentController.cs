using MakanyApplication.Shared.Models.DataTransferObjects.Government;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class GovernmentController : ControllerBase
    {
        private readonly GovernmentRepo _governmenRepo;
        public GovernmentController(GovernmentRepo governmenRepo)
        {
            _governmenRepo = governmenRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGovernment([FromBody] CreateGovernment model)
            => Ok(await _governmenRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateGovernment([FromBody] UpdateGovernment model)
            => Ok(await _governmenRepo.UpdateAsync(model));

        [HttpGet("GetGovernmentForUpdate/{Id:int}")]
        public async Task<IActionResult> GetGovernmentForUpdate(int Id)
            => Ok(await _governmenRepo.GetGovernmentForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _governmenRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _governmenRepo.GetAsync());
    }
}
