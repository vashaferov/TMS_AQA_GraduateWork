using System.Text.Json.Serialization;

namespace GraduateWork.Models;

public class Account
{
    [JsonPropertyName("userId")] public int UserId { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("firstName")] public string FirstName { get; set; }
    [JsonPropertyName("lastName")] public string LastName { get; set; }
    [JsonPropertyName("authType")] public string AuthType { get; set; }
    [JsonPropertyName("tokenExpiresAt")] public string TokenExpiresAt { get; set; }
    [JsonPropertyName("tenantFeatures")] public string[] TenantFeatures { get; set; }
    [JsonPropertyName("sessions")] public Session[] Sessions { get; set; }
}