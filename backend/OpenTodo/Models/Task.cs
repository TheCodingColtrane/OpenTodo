using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus.DataSets;

namespace OpenTodo.Models
{
    public class TaskSchema
    {
        public TaskSchema()
        {
            ID = 0;
            Title = "";
            Description = "";
            IsCompleted = false;
            Category = 0;
            Users = new UserSchema();
        }

        public int ID { get; set; }
        [Column(TypeName = "varchar")]
        [Required]
        public string Title { get; set; }
        [Length(minimumLength: 3, maximumLength: 255)]
        [Required]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        [Column(TypeName = "smallint")]
        [Required]
        public short Category { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateOnly DueDate {get; set;}

        [Column(name: "creator_id")]
        public int CreatorUserId {get; set;}
        public UserSchema Users { get; set; }
        [Column(name: "assigned_user_id")]
        public int AssignedUserId {get; set;}
        public UserSchema AssignedUser { get; set; }
        [Column(name: "board_id")]
        public int BoardId {get; set;}
        public BoardSchema Board { get; set; }


    }
}
