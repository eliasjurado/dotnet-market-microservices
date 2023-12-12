using static Market.Web.Utility.Constants;
namespace Market.Web.Models.Dto
{
    public class RequestDto<T> where T : class
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public T Data { get; set; }
        public string AccessToken { get; set; }

        public RequestDto()
        {

        }
    }
}
