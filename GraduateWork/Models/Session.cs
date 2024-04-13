using System.Text.Json.Serialization;

namespace GraduateWork.Models;

public class Session
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("tenant")] public string Tenant { get; set; }
    [JsonPropertyName("agent")] public string Agent { get; set; }
    [JsonPropertyName("geo")] public string Geo { get; set; }
    [JsonPropertyName("loginAt")] public string LoginAt { get; set; }
}