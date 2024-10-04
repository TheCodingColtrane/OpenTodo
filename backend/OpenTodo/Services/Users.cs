using OpenTodo.Models;
using OpenTodo.Repositories;
using BCrypt.Net;
using OpenTodo.Utils;
using OpenTodo.DTOs;
namespace OpenTodo.Services
{
    public class UserService(UserRepository userRepo)
    {
        private readonly UserRepository _userRepo = userRepo;

        public async Task<List<UserSchema>> GetUsers()
        {
            return await _userRepo.GetAllUsers();
        }
            
         public async Task<List<UserDTO>> SearchUserByTerm(string term)
        {
            var users = await _userRepo.SearchUserByTerm(term);
              var hashID = new HashID();
             List<UserDTO>? taskDTO = [];
             foreach(var user in users){
                
                taskDTO.Add(new UserDTO() {
                    Code = hashID.GenerateHash(user.Id),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
                });
             }

             return taskDTO;
        }
        

        public async Task<UserSchema> GetUser(int id)
        {
            return await _userRepo.GetUser(id);
        }

         public async Task<bool> GetUsernameAvailability(string username)
        {
            return await _userRepo.GetUsernameAvailability(username);
        }

        public async Task<bool> Create(UserSchema user)
        {
            return await _userRepo.Create(user);
        }

  

    }   
}
