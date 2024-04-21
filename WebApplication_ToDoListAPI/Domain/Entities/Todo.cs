using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_ToDoListAPI.Domain.Entities
{
    public class Todo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Guid EventGuid { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
