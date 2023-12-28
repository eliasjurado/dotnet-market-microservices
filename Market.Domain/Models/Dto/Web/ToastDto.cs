namespace Market.Domain.Models.Dto.Web
{
    public sealed class ToastDto
    {
        public string Title { get; set; }
        public KeyValuePair<string, object> Body { get; set; }
        public ToastDto()
        {
            Title = string.Empty;
            Body = new KeyValuePair<string, object>();
        }
    }
}
