using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTodo.Models {
    public class BoardParticipantSchema {
         public int ID {get; set;}
        public int BoardId {get; set;}
        public BoardSchema Boards {get; set;}
        public int UserId {get; set;}
        public UserSchema Users {get; set;}

        public DateTime JoinedAt {get; set;}
    }
}