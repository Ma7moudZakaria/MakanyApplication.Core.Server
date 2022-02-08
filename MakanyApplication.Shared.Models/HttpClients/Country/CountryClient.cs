using MakanyApplication.Shared.Models.DataTransferObjects.Country;
using MakanyApplication.Shared.Models.ErrorHandler;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.HttpClients.Country
{
    public class CountryClient
    {
        private readonly HttpClient _client;

        public CountryClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexCountry>>> GetAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexCountry>>>($"api/Country/GetAll");
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCountry>>
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateCountry>> GetForUpdate(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<CommitResult<UpdateCountry>>($"api/Country/GetCountryForUpdate/{Id}");
            }
            catch
            {
                return new CommitResult<UpdateCountry>
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0022",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> UpdateAsync(UpdateCountry model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PutAsJsonAsync("api/Country", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> CreateAsync(CreateCountry model)
        {
            try
            {
                HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Country", model);
                return await Result.Content.ReadFromJsonAsync<CommitResult>();
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0022",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
