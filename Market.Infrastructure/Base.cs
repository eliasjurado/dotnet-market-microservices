namespace Market.Infrastructure
{
    public static class Base
    {
        public const string DefaultUser = "SYSTEM";
        public const string GlobalSeparator = "\n";
        public static string CouponAPIBase { get; set; } = string.Empty;
        public static string AuthAPIBase { get; set; } = string.Empty;
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
