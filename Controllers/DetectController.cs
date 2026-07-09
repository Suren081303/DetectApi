using Microsoft.AspNetCore.Mvc;
using DetectApi.Models;
using DetectApi.Services;

namespace DetectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetectController : ControllerBase
    {
        private readonly IInformation detectService;
        public DetectController(IInformation detectService)
        {
            this.detectService = detectService;
        }

        [HttpPost]
        public ActionResult<DetectResponse> Detect(DetectRequest request)
        {
            var response = detectService.Detect(request);
            return Ok(response);
        }
    }
}