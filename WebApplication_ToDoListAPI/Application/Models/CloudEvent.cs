namespace WebApplication_ToDoListAPI.Application.Models
{
    public class CloudEvent<T> where T : class
    {
        public Guid Id { get; set; }
        public required string Source { get; set; }
        public required string SpecVersion { get; set; }
        public required string Type { get; set; }
        public string? DataContentType { get; set; } = "application/json";
        public DateTime Time { get; set; }
        public T? Data { get; set; }
    }
}
