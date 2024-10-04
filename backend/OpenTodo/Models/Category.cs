using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenTodo.Models {
public class CategorySchema
{
    public CategorySchema()
    {
        ID = 0;
        Name= "";
    }
   public int ID {get; set;}
    [Required]
    [Column(name: "name", TypeName = "varchar")]
    public string Name {get; set;}
    [Column(name: "created_at")]
    public DateTime CreatedAt {get; set;}
    [Column(name: "updated_at")]
    public DateTime UpdatedAt {get; set;}
    public int BoardId {get; set;}
    [Required]
    [Column(name: "board_id")]
    public BoardSchema Board {get; set;}
}
}
