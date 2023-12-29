namespace Market.Infrastructure
{
    public static class Base
    {
        public const string DefaultUser = "SYSTEM";
        public const string GlobalSeparator = "\r\n";
        public const string TokenCookie = "JWTToken";
        public const string AuthorizationCookie = "Authorization";
        public const string RoleType = "role";
        public const string BearerReferenceType = "Bearer";
        public static string ProductAPIBase { get; set; } = string.Empty;
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
