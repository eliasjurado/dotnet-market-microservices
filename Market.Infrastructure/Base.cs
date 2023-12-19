namespace Market.Infrastructure
{
    public class Base
    {
        public const string DefaultUser = "SYSTEM";
        public static string CouponAPIBase { get; set; }
        public enum ByteType
        {
            No,
            Yes
        }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
