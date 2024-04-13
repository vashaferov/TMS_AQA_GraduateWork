using System.Text.Json.Serialization;

namespace GraduateWork.Models;

public class Project
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("is_deleted")] public bool IsDeleted { get; set; }
    [JsonPropertyName("created_at")] public string CreatedAt { get; set; }
    [JsonPropertyName("created_by")] public int CreatedBy { get; set; }
    [JsonPropertyName("modified_at")] public string ModifiedAt { get; set; }
    [JsonPropertyName("modified_by")] public int ModifiedBy { get; set; }
    [JsonPropertyName("deleted_at")] public string DeletedAt { get; set; }
    [JsonPropertyName("deleted_by")] public int? DeletedBy { get; set; }
    [JsonPropertyName("_etag")] public string Etag { get; set; }
    [JsonPropertyName("owner_user_id")] public int OwnerUserId { get; set; } = 0;
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("project_key")] public string ProjectKey { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
}