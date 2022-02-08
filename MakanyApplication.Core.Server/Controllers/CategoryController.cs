using MakanyApplication.Shared.Models.DataTransferObjects.Category;
using MakanyApplication.Shared.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MakanyApplication.Core.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepo _categoryRepo;
        public CategoryController(CategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory model)
            => Ok(await _categoryRepo.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategory model)
            => Ok(await _categoryRepo.UpdateAsync(model));

        [HttpGet("GetAreaForUpdate/{Id:int}")]
        public async Task<IActionResult> GetCategoryForUpdate(int Id)
            => Ok(await _categoryRepo.GetCategoryForUpdateAsync(Id));

        [HttpGet("GetDetails/{Id:int}")]
        public async Task<IActionResult> GetDetails(int Id)
            => Ok(await _categoryRepo.GetDetailsAsync(Id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _categoryRepo.GetAsync());
    }
}
