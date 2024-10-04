using Microsoft.EntityFrameworkCore;
using OpenTodo.Data;
using OpenTodo.Models;

namespace OpenTodo.Repositories {
    public class AuthRepository(OpenTodoContext db)
    {   
        private readonly OpenTodoContext _db = db;

        public async Task<UserSchema> Login(string username, string password){
            var user =  await _db.Users.Where(c => c.Username == username).Select(c => new UserSchema{
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PasswordHash =  c.PasswordHash}).FirstOrDefaultAsync();

        if(user is null) return new UserSchema();
        
        Auth.Auth auth = new();
        var passwordMatch = auth.PasswordCompare(user.PasswordHash, password);
        if(passwordMatch) return user;
        return new UserSchema();
        }

       
    }

}