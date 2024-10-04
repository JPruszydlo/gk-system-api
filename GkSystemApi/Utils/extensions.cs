using gk_system_api.Models;
using System.Text;

namespace gk_system_api.Utils
{
    public static class extensions
    {
        public static DecodedUrl? DecodeAuthHeader(this HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex).ToLower();
                var password = usernamePassword.Substring(seperatorIndex + 1);

                return new DecodedUrl()
                {
                    Login = username,
                    Password = password
                };
            }
            return null;
        }
        public static bool TryBase64ToBytes(this string value, out byte[] result)
        {
            result = new byte[0];
            if (!value.Contains("base64")) 
                return false;
            var base64 = value.Split(';')[1].Replace("base64,", "");
            result = System.Convert.FromBase64String(base64);
            return true;
        }

        public static string ModifyIfBase64(this string value)
        {
            if (value.Contains("base64"))
                return string.Empty;
            return value;
        }
        public static string GetImageType(this string value)
        {
            if (!value.Contains("base64") || string.IsNullOrEmpty(value))
                return string.Empty;

            var imageType= value.Split(';')[0].Replace("data:", "");
            return imageType;
        }

        public static byte[] Base64ToByte(this string value)
        {
            if (!value.Contains("base64") || string.IsNullOrEmpty(value))
                return new byte[0];

            byte[] result;
            if(value.TryBase64ToBytes(out result))
            {
                return result;
            }
            return new byte[0];
        }
        public static string? BytesToBase64(this byte[] value, string imageType)
        {
            if (value.Length <= 0)
                return null;

            var base64 = Convert.ToBase64String(value);
            var base64String = $"data:{imageType};base64,{base64}";
            return base64String;
        }
    }
}
