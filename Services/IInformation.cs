using DetectApi.Models;

namespace DetectApi.Services
{
    public interface IInformation
    {
        DetectResponse Detect(DetectRequest request);
    }
}