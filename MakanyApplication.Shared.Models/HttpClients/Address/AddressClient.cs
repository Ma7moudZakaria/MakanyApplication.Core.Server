using MakanyApplication.Shared.Models.DataTransferObjects.Address;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.Address
{
    public class AddressClient
    {
        private readonly HttpClient _client;

        public AddressClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexAddress>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexAddress>>>($"api/Address/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexAddress>>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateAddress>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateAddress>>($"api/Address/GetAddressForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateAddress>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateAddress model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/Address", model);
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

        public async Task<CommitResult> CreateAsync(CreateAddress model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Address", model);
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
