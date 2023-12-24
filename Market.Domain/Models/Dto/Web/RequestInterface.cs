using static Market.Infrastructure.Base;
namespace Market.Domain.Models.Dto.Web
{
    public class RequestInterface
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        public RequestInterface()
        {

        }
    }
}
