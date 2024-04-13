using GraduateWork.Clients;
using GraduateWork.Models;
using RestSharp;

namespace GraduateWork.Service;

public class AuthenticationServices : IAuthenticationServices
{
    private readonly RestClientExtended _client;

    public AuthenticationServices(RestClientExtended client)
    {
        _client = client;
    }

    public Task<RestResponse> GetAuthenticationInfo()
    {
        var request = new RestRequest("/api/v1/account/me");

        return _client.ExecuteAsync(request);
    }
}