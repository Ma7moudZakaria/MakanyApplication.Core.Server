using MakanyApplication.Shared.Models.DataTransferObjects.SubCategory;
using MakanyApplication.Shared.Models.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.SubCategory
{
    public class SubCategoryClient
    {
        private readonly HttpClient _client;

        public SubCategoryClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexSubCategory>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexSubCategory>>>($"api/SubCategory/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexSubCategory>>
                {
                    IsSuccess = false,
                    ErrorCode = "SC-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateSubCategory>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateSubCategory>>($"api/SubCategory/GetSubCategoryForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateSubCategory>
                {
                    IsSuccess = false,
                    ErrorCode = "SC-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateSubCategory model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/SubCategory", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "SC-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> CreateAsync(CreateSubCategory model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/SubCategory", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "SC-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
