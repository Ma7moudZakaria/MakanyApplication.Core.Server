using MakanyApplication.Shared.Models.DataTransferObjects.Item;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepo _itemRepo;
        public ItemController(ItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItem model)
            => Ok(await _itemRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItem model)
            => Ok(await _itemRepo.UpdateAsync(model));

        [HttpGet("GetItemForUpdate/{Id:int}")]
        public async Task<IActionResult> GetItemForUpdate(int Id)
            => Ok(await _itemRepo.GetItemForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _itemRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _itemRepo.GetAsync());
    }
}
