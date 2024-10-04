using OpenTodo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Runtime.InteropServices;
using OpenTodo.Models;
using OpenTodo.DTOs;
using OpenTodo.Utils;
namespace OpenTodo.Repositories
{

    public class TaskRepository(OpenTodoContext db)
    {   
        private readonly OpenTodoContext _db = db;
        private readonly TaskDTO dto = new();
        private readonly HashID hashID = new();


        public async Task<List<TaskDTO>> GetAllTasks()
        {

            var tasks = await _db.Tasks.Take(30).Join(_db.Users, b => b.AssignedUserId, u => u.Id,
            (task, participant) => new { 
                TaskID = task.ID,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                Category = task.Category,
                Description = task.Description,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                ParticipantID = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName
            }).ToListAsync();
            List<TaskDTO>? taskDTO = [];
            foreach(var task in tasks){ 
                taskDTO.Add(new TaskDTO(){
                    Code = hashID.GenerateHash(task.TaskID),
                    Title = task.Title,
                    IsCompleted = task.IsCompleted,
                    Category = task.Category,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    AssignedUser = new() {
                        Code = hashID.GenerateHash(task.ParticipantID),
                        FirstName = task.FirstName,
                        LastName = task.LastName
                        }
            });
            }
            return taskDTO;

        }
        public async Task<List<TaskDTO>> GetAllTasksByUser(int id, int boardId)
        {
            var tasks = await _db.Tasks.Where(c => c.AssignedUserId == id && c.Board.ID == boardId).Take(30).OrderByDescending(c => c.ID).ToListAsync();
            List<TaskDTO>? taskDTO = dto.ConvertSchemaToDTO(tasks);
            return taskDTO;

        }

         public async Task<List<TaskDTO>> GetAllTasksByCategory(int category, int boardId, int? pageId)
        {
            var tasks = new List<TaskSchema>();
            if(pageId is null){
                 tasks = await _db.Tasks.Where(c => c.Category == category && c.BoardId == boardId).Take(30).OrderByDescending(c => c.ID).ToListAsync();
            } else {
                 tasks = await _db.Tasks.Where(c => c.Category == category && c.BoardId == boardId && c.ID < pageId).Take(30).ToListAsync();
            }
            List<TaskDTO>? taskDTO = dto.ConvertSchemaToDTO(tasks);
            return taskDTO;

        }


     
        public async Task<TaskDTO> GetTasksById(int id)
        {
            var task = await _db.Tasks.Where(c => c.ID == id).FirstAsync();
            return dto.ConvertSchemaToDTO(new List<TaskSchema>(){task})[0];

        }   


        public async Task<string> Create(TaskSchema newTask)
        {
            try
            {
                var task = _db.Tasks.AddAsync(newTask);
                var createdTask = _db.SaveChangesAsync();
                await Task.WhenAll([task.AsTask(), createdTask]);
                var code = hashID.GenerateHash(newTask.ID);
                return code;
            }
            catch (Exception)
            {
                return "";
                throw;
            }


        }

        public async Task<bool> Update(TaskSchema task)
        {
            var currentTask = await _db.Tasks.FindAsync(task.ID);
            if (currentTask is not null)
            {
                task = currentTask;
                _db.Update(task);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;



        }

        public async Task<bool> Delete(int id)
        {
            var currentTodo= await _db.Tasks.FindAsync(id);
            if (currentTodo is not null)
            {
                _db.Remove(currentTodo);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
