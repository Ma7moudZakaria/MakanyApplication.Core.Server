using MakanyApplication.Shared.Models.DataTransferObjects.Category;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.Category
{
    public class CategoryClient
    {
        private readonly HttpClient _client;

        public CategoryClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexCategory>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexCategory>>>($"api/Category/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCategory>>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateCategory>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateCategory>>($"api/City/GetCategoryForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateCategory>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateCategory model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/Category", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> CreateAsync(CreateCategory model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Category", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
