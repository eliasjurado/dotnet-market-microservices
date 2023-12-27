using Market.Domain.Models.Dto.Web;
using Market.Infrastructure;
using Market.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Market.Infrastructure.Base;

namespace Market.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseInterface> SendAsync(RequestInterface request, bool isSecured = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MarketAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                if (isSecured)
                {
                    message.Headers.Add(AuthorizationCookie, $"{BearerReferenceType} {_tokenProvider.GetToken()}");
                }

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
                    case HttpStatusCode.NotFound:
                        return new() { StatusCode = HttpStatusCode.NotFound, Status = Format.GetName(nameof(HttpStatusCode.NotFound)), IsSuccess = false, Message = Format.GetName(nameof(HttpStatusCode.NotFound)), Metadata = new List<string> { Format.GetName(nameof(HttpStatusCode.NotFound)) } };
                    case HttpStatusCode.Unauthorized:
                        return new() { StatusCode = HttpStatusCode.Unauthorized, Status = Format.GetName(nameof(HttpStatusCode.Unauthorized)), IsSuccess = false, Message = Format.GetName(nameof(HttpStatusCode.Unauthorized)), Metadata = new List<string> { Format.GetName(nameof(HttpStatusCode.Unauthorized)) } };
                    case HttpStatusCode.Forbidden:
                        return new() { StatusCode = HttpStatusCode.Forbidden, Status = Format.GetName(nameof(HttpStatusCode.Forbidden)), IsSuccess = false, Message = Format.GetName(nameof(HttpStatusCode.Forbidden)), Metadata = new List<string> { Format.GetName(nameof(HttpStatusCode.Forbidden)) } };
                    case HttpStatusCode.InternalServerError:
                        return new() { StatusCode = HttpStatusCode.InternalServerError, Status = Format.GetName(nameof(HttpStatusCode.InternalServerError)), IsSuccess = false, Message = Format.GetName(nameof(HttpStatusCode.InternalServerError)), Metadata = new List<string> { Format.GetName(nameof(HttpStatusCode.InternalServerError)) } };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseInterface>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {

                return new ResponseInterface
                {
                    IsSuccess = false,
                    Metadata = Format.GetInnerExceptionMessage(ex)
                };
            }
        }


    }
}