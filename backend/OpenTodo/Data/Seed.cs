using Bogus;
using Microsoft.EntityFrameworkCore;
using OpenTodo.Models;

namespace OpenTodo.Data
{
    public class Seed(OpenTodoContext db)
    {
        private readonly OpenTodoContext _db = db;

        public async Task Start()
        {

            var users = new Faker<UserSchema>()
                .RuleFor(c => c.Id, f => f.UniqueIndex)
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Username, f => f.Person.UserName)
                .RuleFor(c => c.DOB, f => f.Date.PastDateOnly())
                .RuleFor(c => c.CreatedAt, f => f.Date.Past().ToUniversalTime())
                .RuleFor(c => c.PasswordHash, f => f.Hashids.Encode(f.UniqueIndex))
                .Generate(10000);
                
            var boards = new Faker<BoardSchema>()
            .RuleFor(c => c.ID, f => f.UniqueIndex)
            .RuleFor(c => c.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.CreatedAt, f => f.Date.Past().ToUniversalTime())
            .RuleFor(c => c.User, () => new Faker<UserSchema>().RuleFor(x => x.Id, q => q.UniqueIndex))
            .Generate(10000);

            var tasks = new Faker<TaskSchema>()
                .RuleFor(c => c.ID, f => f.UniqueIndex)
                .RuleFor(c => c.Title, f => f.Hacker.Phrase())
                .RuleFor(c => c.Description, f => f.Lorem.Sentences())
                .RuleFor(c => c.IsCompleted, f => f.Random.Bool())
                .RuleFor(c => c.Category, f => f.Random.Byte(0, 6))
                .RuleFor(c => c.CreatedAt, f => f.Date.Past().ToUniversalTime())
                .RuleFor(c => c.Users, () => new Faker<UserSchema>().RuleFor(x => x.Id, q => q.UniqueIndex))
                .RuleFor(c => c.Board, () => new Faker<BoardSchema>().RuleFor(x => x.ID, q => q.UniqueIndex))
                .RuleFor(c => c.AssignedUser, () => new Faker<UserSchema>().RuleFor(x => x.Id, q => q.UniqueIndex))
                .RuleFor(c => c.DueDate, f => f.Date.FutureDateOnly())
                .Generate(10000);

          

            int i = 0;
            await _db.Users.AddRangeAsync(users);
            await _db.SaveChangesAsync();
            await _db.Boards.AddRangeAsync(boards);
            await _db.SaveChangesAsync();
            var newEntries = db.ChangeTracker.Entries().Where(e => e.State != EntityState.Detached).ToList();
            foreach (var entry in newEntries) entry.State = EntityState.Detached;
            foreach(var task in tasks) {
                tasks[i].Board.ID = boards[i].ID;
                tasks[i].Users.Id = users[i].Id;
                tasks[i].AssignedUser.Id = users[i].Id;
                await _db.Database.ExecuteSqlAsync($"INSERT INTO task VALUES ({task.ID}, {task.Title}, {task.Description}, {task.IsCompleted}, {task.Category}, {task.CreatedAt}, {task.CreatedAt}, {task.DueDate}, {task.Users.Id}, {task.AssignedUser.Id}, {task.Board.ID})");
                 i++;
            }
       

            //await Task.WhenAll([usersTask, todosTask, savedChanges]);


        }
    }
}
