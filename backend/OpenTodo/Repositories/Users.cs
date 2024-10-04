using OpenTodo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Runtime.InteropServices;
using OpenTodo.Models;
using OpenTodo.DTOs;
using OpenTodo.Utils;
namespace OpenTodo.Repositories
{

    public class UserRepository(OpenTodoContext db)
    {
        private readonly OpenTodoContext _db = db;

        public async Task<List<UserSchema>> GetAllUsers()
        { 
            return await _db.Users.Select(c => new UserSchema
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DOB = c.DOB,
                Username = c.Username,
                CreatedAt = c.CreatedAt
            }).ToListAsync();
        }

        public async Task<UserSchema> GetUser(int id)
        {
            return await _db.Users.Select(c => new UserSchema
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DOB = c.DOB,
                Username = c.Username,
                CreatedAt = c.CreatedAt
            }).Where(c => c.Id == id).FirstAsync();

        }

        public async Task<bool> GetUsernameAvailability(string username)
        {
            return await _db.Users.Where(c => c.Username == username).AnyAsync();

        }

        
        public async Task<List<UserSchema>> SearchUserByTerm(string term)
        {
            var users = await _db.Users.Where(c =>  EF.Functions.ILike(c.FirstName, $"%{term}%") || EF.Functions.ILike(c.LastName, $"%{term}%") || EF.Functions.ILike(c.Username, $"%{term}%")).
            Select(c => new UserSchema
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Username = c.Username
            }).Take(10).ToListAsync();
            return users;

        }


        public async Task<bool> Create(UserSchema newUser)
        {
            try
            {
                Auth.Auth auth = new();
                newUser.PasswordHash = auth.GenerateHash(newUser.PasswordHash);
                newUser.CreatedAt = DateTime.Now.ToUniversalTime();
                var user = _db.Users.AddAsync(newUser);
                var createdUser = _db.SaveChangesAsync();
                await Task.WhenAll([user.AsTask(), createdUser]);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
           

        }

        public async Task<bool> Update(UserSchema user)
        {
            var currentUser = await _db.Users.FindAsync(user.Id);
            if (currentUser is not null)
            {
                currentUser = user;
                _db.Update(currentUser);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;

            
          
        }

        public async Task<bool> Delete(int id)
        {
            var currentUser = await _db.Users.FindAsync(id);
            if (currentUser is not null)
            {
                _db.Remove(currentUser);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
