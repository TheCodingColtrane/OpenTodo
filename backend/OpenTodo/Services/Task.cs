using OpenTodo.DTOs;
using OpenTodo.Models;
using OpenTodo.Repositories;
using OpenTodo.Utils;

namespace OpenTodo.Services
{
    public class TaskService(TaskRepository taskRepo)
    {
        private readonly TaskRepository _taskRepo = taskRepo;
        private readonly HashID hashID = new();
        public async Task<List<TaskDTO>> GetAllTasks()
        {
            return await _taskRepo.GetAllTasks();

        }

        public async Task<List<TaskDTO>> GetAllTasksByUserId(string code, int id)
        {   
            int boardId = hashID.ReverseHash(code); 
            return await _taskRepo.GetAllTasksByUser(id, boardId);
        }


          public async Task<List<TaskDTO>> GetAllTasksByCategory(string categoryCode, string boardCode, string? pageCode)
        {   
            int category = hashID.ReverseHash(categoryCode);
            int boardId = hashID.ReverseHash(boardCode);
            int? pageId = pageCode is not null ? hashID.ReverseHash(boardCode) : null;
            return await _taskRepo.GetAllTasksByCategory(category, boardId, pageId);
        }

        public async Task<TaskDTO> GetTasksById(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return new();
            return await _taskRepo.GetTasksById(id);
        }

        public async Task<string> Create(TaskDTO taskDTO, UserSchema user){

            var taskSchema = new TaskSchema(){
                Title = taskDTO.Title, Description = taskDTO.Description,
                Category = taskDTO.Category, IsCompleted = taskDTO.IsCompleted,
                DueDate = taskDTO.DueDate,
                AssignedUserId = hashID.ReverseHash(taskDTO.AssignedUser.Code),
                BoardId = hashID.ReverseHash(taskDTO.Board.Code),
                CreatedAt = DateTime.Now.ToUniversalTime(),
                CreatorUserId = user.Id
                } ;
            taskSchema.BoardId = hashID.ReverseHash(taskDTO.Board.Code);
            return await _taskRepo.Create(taskSchema);
        }

         public async Task<bool> Update(TaskDTO taskDTO, UserSchema user){

            var taskSchema = new TaskSchema(){
                ID = hashID.ReverseHash(taskDTO.Code),
                Title = taskDTO.Title, Description = taskDTO.Description,
                Category = taskDTO.Category, IsCompleted = taskDTO.IsCompleted,
                DueDate = taskDTO.DueDate,
                AssignedUserId = hashID.ReverseHash(taskDTO.AssignedUser.Code),
                BoardId = hashID.ReverseHash(taskDTO.Board.Code),
                CreatedAt = DateTime.Now.ToUniversalTime(),
                CreatorUserId = user.Id
                } ;
            taskSchema.BoardId = hashID.ReverseHash(taskDTO.Board.Code);
            return await _taskRepo.Update(taskSchema);
         }
         public async Task<bool> Delete(string code){
             int id = hashID.ReverseHash(code);
            if(id == 0) return new();
            return await _taskRepo.Delete(id);
        }
        
    }
}
