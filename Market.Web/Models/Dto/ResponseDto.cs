using Market.Infrastructure;
using System.Net;

namespace Market.Web.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public Guid CorrelationId { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Status { get; set; }
        public ICollection<string> Errors { get; set; }

        public ResponseDto()
        {
            CorrelationId = Guid.NewGuid();
            Message = string.Empty;
            IsSuccess = false;
            StatusCode = HttpStatusCode.BadRequest;
            Status = Format.GetName(nameof(HttpStatusCode.BadRequest));
            Errors = new List<string>();
        }
    }
}
