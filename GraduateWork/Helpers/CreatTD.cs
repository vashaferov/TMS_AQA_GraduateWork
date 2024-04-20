using System.Reflection;
using Allure.Net.Commons;
using GraduateWork.Clients;
using GraduateWork.Models;
using Newtonsoft.Json;
using NUnit.Allure.Attributes;
using RestSharp;

namespace GraduateWork.Helpers;

public class CreatTD
{
    private readonly RestClientExtended _client;
    private string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    
    public CreatTD(RestClientExtended client)
    {
        _client = client;
    }
    
    [AllureStep("Подготовка тестовых данных: Проект")]
    public string CreatProject()
    {
        Project project = new Project()
        {
            Name = $"Test {DateTime.Now}",
            Description = "Test Description",
            Etag = "test"
        };
        
        var request = new RestRequest("/api/v1/project", Method.Post)
            .AddJsonBody(project);

        var json = JsonConvert.SerializeObject(_client.ExecuteAsync<Project>(request).Result, Formatting.Indented);
        
        
        string filePath = Path.Combine(assemblyPath, "Resources", "TD", $"Project_{DateTime.Now.ToFileTime()}.json");
        
        File.WriteAllText(filePath, json);
        AllureApi.AddAttachment("Project_TD.json", "json", filePath);

        return filePath;
    }

    public void ClearTD()
    {
        Directory.Delete(Path.Combine(assemblyPath, "Resources", "TD"), true);
    }
}