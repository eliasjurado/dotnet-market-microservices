using System.Text;

namespace Market.Web.Utility
{
    public static class Format
    {
        public static string GetName(string name)
        {
            var sb = new StringBuilder();
            sb.Append(name.First().ToString().ToUpper());
            name.Substring(1).ToList().ForEach(x => sb.Append(char.IsUpper(x) ? $" {x}" : x.ToString()));
            return sb.ToString();
        }

        public static ICollection<string> GetInnerExceptionMessage(Exception exception)
        {
            List<string> messages = new();

            if (exception.InnerException == null)
                messages.Add(exception.Message);

            while (exception.InnerException != null)
            {
                messages.Add(exception.InnerException.Message);
                exception = exception.InnerException;
            }

            return messages;
        }

    }
}
