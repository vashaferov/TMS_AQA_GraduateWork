using System.Net;
using GraduateWork.Models;
using RestSharp;

namespace GraduateWork.Service;

public interface IProjectServices
{
    Task<Project> GetProject(int projectId);
    Task<RestResponse> AddProject(Project project);
    Task<RestResponse> UpdateProject(Project project);
    HttpStatusCode DeleteProject(int projectId);
}