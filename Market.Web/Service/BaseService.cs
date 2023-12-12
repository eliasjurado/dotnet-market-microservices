using Market.Web.Models.Dto;
using Market.Web.Service.IService;
using Market.Web.Utility;
using Newtonsoft.Json;
using System.Text;
using static Market.Web.Utility.Constants;

namespace Market.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto> SendAsync(RequestDto request)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MarketAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(request.Url);

                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (request.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;

                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Errors = new List<string> { "Access Denied" } };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Errors = new List<string> { "Unauthorized" } };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Errors = new List<string> { "Internal Server Error" } };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    IsSuccess = false,
                    Errors = Format.GetInnerExceptionMessage(ex)
                };
            }
        }


    }
}