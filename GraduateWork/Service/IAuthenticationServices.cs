using RestSharp;

namespace GraduateWork.Service;

public interface IAuthenticationServices
{
    Task<RestResponse> GetAuthenticationInfo();
}