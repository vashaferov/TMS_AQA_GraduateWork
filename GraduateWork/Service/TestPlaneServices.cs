using System.Net;
using GraduateWork.Clients;
using GraduateWork.Models;
using RestSharp;

namespace GraduateWork.Service;

public class TestPlaneServices : ITestPlaneServices
{
    private readonly RestClientExtended _client;

    public TestPlaneServices(RestClientExtended client)
    {
        _client = client;
    }

    public Task<TestPlan> GetTestPlan(int testPlanId)
    {
        var request = new RestRequest("api/v1/testplan/{testPlanId}")
            .AddUrlSegment("testPlanId", testPlanId);

        return _client.ExecuteAsync<TestPlan>(request);
    }

    public Task<TestPlan> AddTestPlan(TestPlan testPlan)
    {
        var request = new RestRequest("/api/v1/testplan", Method.Post)
            .AddJsonBody(testPlan);

        return _client.ExecuteAsync<TestPlan>(request);
    }

    public Task<RestResponse> UpdateTestPlan(TestPlan testPlan)
    {
        var request = new RestRequest("api/v1/testplan/{testPlanId}", Method.Put)
            .AddUrlSegment("testPlanId", testPlan.Id)
            .AddJsonBody(testPlan);

        return _client.ExecuteAsync(request);
    }

    public HttpStatusCode DeleteTestPlan(int testPlanId)
    {
        var request = new RestRequest("api/v1/testplan/{testPlanId}", Method.Delete)
            .AddUrlSegment("testPlanId", testPlanId);

        return _client.ExecuteAsync(request).Result.StatusCode;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}