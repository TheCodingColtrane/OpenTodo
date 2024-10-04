using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTodo.Models {
public class BoardSchema
{
    public BoardSchema()
    {
        ID = 0;
        Name=  "";
    }
    
  public int ID { get; set; }

    [Required]
    [Column(name: "name", TypeName = "varchar")]
    public string Name { get; set; }

    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; }

    [Column(name: "updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Foreign key to User
    [Column(name: "user_id")]
    public int UserId { get; set; }  // Add foreign key explicitly

    public UserSchema User { get; set; }

    public ICollection<BoardParticipantSchema> BoardParticipants {get; } = new List<BoardParticipantSchema>();
    public ICollection<TaskSchema> TaskParticipants {get; }  = new List<TaskSchema>();

}
}
