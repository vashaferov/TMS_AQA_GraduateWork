using System.Net;
using GraduateWork.Clients;
using GraduateWork.Models;
using RestSharp;

namespace GraduateWork.Service;

public class ProjectServices : IProjectServices
{
    private readonly RestClientExtended _client;

    public ProjectServices(RestClientExtended client)
    {
        _client = client;
    }

    public Task<Project> GetProject(int projectId)
    {
        var request = new RestRequest("/api/v1/project/{projectId}")
            .AddUrlSegment("projectId", projectId);

        return _client.ExecuteAsync<Project>(request);
    }

    public Task<RestResponse> AddProject(Project project)
    {
        var request = new RestRequest("/api/v1/project", Method.Post)
            .AddJsonBody(project);

        return _client.ExecuteAsync(request);
    }

    public Task<RestResponse> UpdateProject(Project project)
    {
        var request = new RestRequest("/api/v1/project/{projectId}", Method.Put)
            .AddUrlSegment("projectId", project.Id)
            .AddJsonBody(project);

        return _client.ExecuteAsync(request);
    }

    public HttpStatusCode DeleteProject(int projectId)
    {
        var request = new RestRequest("api/v1/project/{projectId}", Method.Delete)
            .AddUrlSegment("projectId", projectId);

        return _client.ExecuteAsync(request).Result.StatusCode;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}