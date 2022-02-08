using MakanyApplication.Shared.Models.DataTransferObjects.Area;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.Area
{
    public class AreaClient
    {
        private readonly HttpClient _client;

        public AreaClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexArea>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexArea>>>($"api/Area/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexArea>>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateArea>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateArea>>($"api/Area/GetAreaForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateArea>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateArea model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/Area", model);
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

        public async Task<CommitResult> CreateAsync(CreateArea model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Area", model);
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
