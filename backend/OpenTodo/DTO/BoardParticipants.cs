using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTodo.DTOs {
    public class BoardParticipantDTO {

        public BoardParticipantDTO()
        {
            Code = "";

        }
        public string Code {get; set;}
        [Required]
        [Column(name: "user_id")]
        public required UserDTO Users {get; set;}
        [Required]
        [Column(name: "board_id")]
        public required BoardDTO Boards {get; set;}
        public DateTime JoinedAt {get; set;}
    }
}