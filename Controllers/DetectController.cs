using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DetectApi.Models;

namespace DetectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetectController : ControllerBase
    {
        [HttpPost]
        public ActionResult<DetectResponse> Detect(DetectRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Value))
            {
                return Ok(new DetectResponse
                {
                    Type = "Unknown",
                    Provider = null
                });
            }
            string value = request.Value.Trim();
            DetectResponse response = new DetectResponse();
            if (IsEmail(value))
            {
                response.Type = "Email";
                response.Provider = null;
            }
            else if (IsPhone(value))
            {
                response.Type = "Phone";
                response.Provider = GetArmenianOperator(value);
            }
            else if (IsTimeOnly(value))
            {
                response.Type = "Time";
                response.Provider = null;
            }
            else if (IsDateOrDateTime(value, out string dateType))
            {
                response.Type = dateType;
                response.Provider = null;
            }
            else
            {
                response.Type = "Unknown";
                response.Provider = null;
            }
            return Ok(response);
        }
        private bool IsEmail(string value)
        {
            return MailAddress.TryCreate(value, out _);
        }
        private bool IsPhone(string value)
        {
            string pattern = @"^\+374\d{8}$";
            return Regex.IsMatch(value, pattern);
        }
        private string GetArmenianOperator(string value)
        {
            string prefix = value.Substring(4, 2);
            if (prefix == "33" || prefix == "97")
            {
                return "Team Telecom Armenia";
            }
            if (prefix == "77" || prefix == "93" || prefix == "94" || prefix == "98")
            {
                return "Viva";
            }
            if (prefix == "43" || prefix == "55" || prefix == "44")
            {
                return "Ucom";
            }
            return "Unknown";
        }
        private bool IsTimeOnly(string value)
        {
            if (!value.Contains(":"))
            {
                return false;
            }
            bool success = TimeOnly.TryParse(value, out _);
            return success;
        }
        private bool IsDateOrDateTime(string value, out string dateType)
        {
            dateType = "Unknown";
            bool success = DateTime.TryParse(value, out _);
            if (!success)
            {
                return false;
            }
            if (value.Contains(" ") || value.Contains("T"))
            {
                dateType = "DateTime";
            }
            else
            {
                dateType = "Date";
            }
            return true;
        }
    }
}
