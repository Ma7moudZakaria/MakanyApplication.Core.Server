using MakanyApplication.Shared.Models.DataTransferObjects.Government;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.Government
{
    public class GovernmentClient
    {
        private readonly HttpClient _client;

        public GovernmentClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexGovernment>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexGovernment>>>($"api/Government/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexGovernment>>
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateGovernment>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateGovernment>>($"api/Government/GetGovernmentForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateGovernment>
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateGovernment model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/Government", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> CreateAsync(CreateGovernment model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Government", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
