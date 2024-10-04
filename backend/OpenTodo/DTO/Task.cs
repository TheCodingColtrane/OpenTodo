using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus.DataSets;
using OpenTodo.Models;
using OpenTodo.Utils;

namespace OpenTodo.DTOs
{
    public class TaskDTO
    {
        
        public readonly List<TaskSchema> taskSchema;
        readonly List<TaskDTO> TaskList = [];

        public TaskDTO()
        {
            Code = "";
            Title = "";
            Description = "";
            IsCompleted = false;
            Category = 0;
            //Users = new UserDTO();
        
        }


        public List<TaskDTO> ConvertSchemaToDTO(List<TaskSchema> tasks){
               var hash = new HashID();
           
             foreach(var task in tasks){
                TaskList.Add(new TaskDTO()  {
                    Code = hash.GenerateHash(task.ID),
                    Title = task.Title,
                    Description = task.Description,
                    IsCompleted = task.IsCompleted,
                    Category = task.Category,
                    CreatedAt = task.CreatedAt,
                    UpdatedAt = task.UpdatedAt,
                    DueDate = task.DueDate,
                    AssignedUser = task.AssignedUser == null ? 
                    new UserDTO() :
                    new UserDTO() {
                        Code = hash.GenerateHash(task.AssignedUser.Id),
                        FirstName = task.AssignedUser.FirstName,
                        LastName = task.AssignedUser.LastName,
                        DOB = task.AssignedUser.DOB,
                        Username = task.AssignedUser.Username,
                        CreatedAt = task.CreatedAt
                    },
                    Board = task.Board == null ? 
                    new BoardDTO() :
                    new BoardDTO(){
                        Code = hash.GenerateHash(task.Board.ID),
                        Name = task.Board.Name,
                        CreatedAt = task.Board.CreatedAt,
                        UpdatedAt = task.Board.UpdatedAt,
                        User =  task.AssignedUser == null ? 
                    new UserDTO() :
                    new UserDTO() {
                        Code = hash.GenerateHash(task.Users.Id),
                        FirstName = task.Users.FirstName,
                        LastName = task.Users.LastName,
                        DOB = task.Users.DOB,
                        Username = task.Users.Username,
                        CreatedAt = task.CreatedAt
                        },

                    }
                    // Board = task.Board,
                    // AssignedUser = task.AssignedUser

                });
        };

        return TaskList;
        }
        public string Code { get; set; }
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
        [Column(name: "user_id")]
        [Required]
        public  UserDTO? Users { get; set; }
        [Column(name: "assigned_user_id")]
        [Required]
        public  UserDTO? AssignedUser { get; set; }
        [Column(name: "board_id")]
        [Required]
        public  BoardDTO? Board { get; set; }


    }
}
