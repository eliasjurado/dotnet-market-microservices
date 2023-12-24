using Market.Infrastructure;
using System.Net;

namespace Market.Services.AuthAPI.Models
{
    public class ResponseDto<T> where T : new()
    {
        public bool IsSuccess { get; set; }
        public Guid CorrelationId { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ICollection<string> Metadata { get; set; }

        public ResponseDto()
        {
            CorrelationId = Guid.NewGuid();
            Message = string.Empty;
            IsSuccess = false;
            Data = new T();
            StatusCode = HttpStatusCode.BadRequest;
            Status = Format.GetName(nameof(HttpStatusCode.BadRequest));
            Metadata = new List<string>();
        }

    }
}
