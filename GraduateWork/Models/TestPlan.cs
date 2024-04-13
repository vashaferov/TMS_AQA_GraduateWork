using System.Text.Json.Serialization;

namespace GraduateWork.Models;

public class TestPlan
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("is_deleted")] public bool IsDeleted { get; set; }
    [JsonPropertyName("project_id")] public int ProjectId { get; set; }
    [JsonPropertyName("created_at")] public string CreatedAt { get; set; }
    [JsonPropertyName("created_by")] public int CreatedBy { get; set; }
    [JsonPropertyName("modified_at")] public string ModifiedAt { get; set; }
    [JsonPropertyName("modified_by")] public int ModifiedBy { get; set; }
    [JsonPropertyName("deleted_at")] public string DeletedAt { get; set; }
    [JsonPropertyName("deleted_by")] public int? DeletedBy { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; }
}