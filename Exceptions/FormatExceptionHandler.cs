using DetectApi.Models;

namespace DetectApi.Exceptions
{
    public class FormatExceptionHandler
    {
        private string GenerateMessage(string type, string? example = null)
        {
            if (string.IsNullOrEmpty(example))
            {
                return $"This is type like of {type}. Please write correct!";
            }
            return $"This is type like of {type}. Please write correct! {example}";
        }

        public DetectResponse TypeHandler(string value)
        {
            if (value.Contains("@"))
            {
                return new DetectResponse
                {
                    Type = "Unknown",
                    Message = GenerateMessage("Email", "example@example.com")
                };
            }

            if (value.Contains(":"))
            {
                return new DetectResponse
                {
                    Type = "Unknown",
                    Message = GenerateMessage("Time", "14:30")
                };
            }

            if (value.Contains("-") || value.Contains("/"))
            {
                return new DetectResponse
                {
                    Type = "Unknown",
                    Message = GenerateMessage("Date", "31-12-2026")
                };
            }

            if ((value.StartsWith("+374") || value.StartsWith("0")) && value.Any(char.IsDigit))
            {
                return new DetectResponse
                {
                    Type = "Unknown",
                    Message = GenerateMessage("Phone", "+374XXXXXXXX")
                };
            }

            return new DetectResponse
            {
                Type = "Unknown",
                Message = "Format could not be detected."
            };
        }
    }
}
