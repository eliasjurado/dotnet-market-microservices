using Market.Web.Utility;
using System.Net;

namespace Market.Web.Models.Dto
{
    public class ResponseDto<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public Guid CorrelationId { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Status { get; set; }
        public ICollection<string> Errors { get; set; }

        public ResponseDto()
        {
            CorrelationId = Guid.NewGuid();
            IsSuccess = false;
            StatusCode = HttpStatusCode.BadRequest;
            Status = Format.GetName(nameof(HttpStatusCode.BadRequest));
            Errors = new List<string>();
        }
    }
}
