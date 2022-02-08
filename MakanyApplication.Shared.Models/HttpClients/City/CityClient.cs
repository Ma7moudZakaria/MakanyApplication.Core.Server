using MakanyApplication.Shared.Models.DataTransferObjects.City;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.City
{
    public class CityClient
    {
        private readonly HttpClient _client;

        public CityClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexCity>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexCity>>>($"api/City/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCity>>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateCity>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateCity>>($"api/City/GetCityForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateCity>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateCity model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/City", model);
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

        public async Task<CommitResult> CreateAsync(CreateCity model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/City", model);
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
