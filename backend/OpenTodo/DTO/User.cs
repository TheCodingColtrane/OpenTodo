using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTodo.DTOs
{
    public class UserDTO
    {

        public UserDTO()
        {
            Code = "";
            FirstName = "";
            LastName = "";
            Username = "";
            PasswordHash = "";
        }
        [Column("id")]
        public string Code { get; set; }
        [Column(name: "first_name", TypeName = "varchar")]
        [Required]
        [Length(minimumLength: 3, maximumLength: 30)]
        public string FirstName{ get; set; }
        [Column(name: "last_name", TypeName = "varchar")]
        [Required]
        [Length(minimumLength: 3, maximumLength: 30)]
        public string LastName { get; set; }
        [Column(name: "dob")]
        public DateOnly DOB { get; set; }
        [Column(name: "username", TypeName = "varchar")]
        [Required]
        [Length(minimumLength: 3, maximumLength: 30)]
        public string Username { get; set; }
        [Column(name: "password_hash", TypeName = "varchar")]
        [Required]
        [Length(minimumLength: 3, maximumLength: 200)]
        public string PasswordHash { get; set; }
        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }

    }
}

