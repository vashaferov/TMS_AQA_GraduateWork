using System.Net;
using GraduateWork.Models;
using RestSharp;

namespace GraduateWork.Service;

public interface ITestPlaneServices
{
    Task<TestPlan> GetTestPlan(int testPlanId);
    Task<TestPlan> AddTestPlan(TestPlan testPlan);
    Task<RestResponse> UpdateTestPlan(TestPlan testPlan);
    HttpStatusCode DeleteTestPlan(int testPlanId);
}