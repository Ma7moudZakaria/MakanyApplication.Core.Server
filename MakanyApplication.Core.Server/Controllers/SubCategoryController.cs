using MakanyApplication.Shared.Models.DataTransferObjects.SubCategory;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryRepo _subCategoryRepo;
        public SubCategoryController(SubCategoryRepo subCategoryRepo)
        {
            _subCategoryRepo = subCategoryRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateSubCategory model)
            => Ok(await _subCategoryRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateSubCategory model)
            => Ok(await _subCategoryRepo.UpdateAsync(model));

        [HttpGet("GetSubCategoryForUpdate/{Id:int}")]
        public async Task<IActionResult> GetItemForUpdate(int Id)
            => Ok(await _subCategoryRepo.GetSubCategoryForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _subCategoryRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _subCategoryRepo.GetAsync());
    }
}
