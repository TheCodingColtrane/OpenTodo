using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenTodo.Models;
using OpenTodo.Utils;

namespace OpenTodo.DTOs {
public class BoardDTO
{
    public BoardDTO()
    {
        Code = "";
        Name=  "";
    }
    
    public string Code {get; set;}
    [Required]
    [Column(name: "name", TypeName = "varchar")]
    public string Name {get; set;}
    [Column(name: "created_at")]
    public DateTime CreatedAt {get; set;}
    [Column(name: "updated_at")]
    public DateTime UpdatedAt {get; set;}
    [Column(name: "user_id")]
    public UserDTO? User {get; set;}


public List<BoardDTO> ConvertSchemaToDTO(List<BoardSchema> boards){
    List<BoardDTO> boardDTOs = [];
    var hash = new HashID();
    foreach(var board in boards){
    boardDTOs.Add(new BoardDTO(){
                Name = board.Name,
                Code = hash.GenerateHash(board.ID),
                CreatedAt = board.CreatedAt,
                UpdatedAt = board.UpdatedAt,
                User = board.User == null ?
                new UserDTO() : 
                new UserDTO(){
                Code = hash.GenerateHash(board.User.Id),
                FirstName = board.User.FirstName,
                LastName = board.User.LastName,
                Username = board.User.Username,
                CreatedAt = board.User.CreatedAt,
                DOB = board.User.DOB,
                PasswordHash = board.User.PasswordHash
            }
});
}

    return boardDTOs;
    
}


}

}