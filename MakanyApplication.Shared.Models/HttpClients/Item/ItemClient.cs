using MakanyApplication.Shared.Models.DataTransferObjects.Item;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.Item
{
    public class ItemClient
    {
        private readonly HttpClient _client;

        public ItemClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexItem>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexItem>>>($"api/Item/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexItem>>
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateItem>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateItem>>($"api/Item/GetItemForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateItem>
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateItem model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/Item", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> CreateAsync(CreateItem model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Item", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
