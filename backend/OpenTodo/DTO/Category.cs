using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenTodo.Models;
using OpenTodo.Utils;

namespace OpenTodo.DTOs {
public class CategoryDTO
{
    public CategoryDTO()
    {
        Code = "";
        Name= "";
    }
   public string Code {get; set;}
    [Required]
    [Column(name: "name", TypeName = "varchar")]
    public string Name {get; set;}
    public int Value {get; set;}
    [Column(name: "created_at")]
    public DateTime CreatedAt {get; set;}
    [Column(name: "updated_at")]
    public DateTime UpdatedAt {get; set;}
    [Required]
    [Column(name: "board_id")]
    public BoardDTO? Board {get; set;}

    public List<CategoryDTO> ConvertSchemaToDTO(List<CategorySchema> categories){
    var hash = new HashID();
    List<CategoryDTO> categoriesDTO = [];
     foreach(var category in categories){
                categoriesDTO.Add(new CategoryDTO() {
                    Name = category.Name,
                    Code = hash.GenerateHash(category.ID),
                    Value = category.ID,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    Board = category.Board == null ? 
                    new BoardDTO() : 
                    new BoardDTO() {
                    Name = category.Board.Name,
                    Code = hash.GenerateHash(category.Board.ID),
                    CreatedAt = category.Board.CreatedAt,
                    UpdatedAt = category.Board.UpdatedAt,
                    User = category.Board.User == null ?
                    new UserDTO() : 
                    new UserDTO(){
                        Code = hash.GenerateHash(category.Board.User.Id),
                        FirstName = category.Board.User.FirstName,
                        LastName = category.Board.User.LastName,
                        Username = category.Board.User.Username,
                        CreatedAt = category.Board.User.CreatedAt,
                        DOB = category.Board.User.DOB,
                        PasswordHash = category.Board.User.PasswordHash
                    }
                    }
                });
}

return categoriesDTO;

}
}


}
