using OpenTodo.Models;
using OpenTodo.Repositories;
using BCrypt.Net;
namespace OpenTodo.Services
{
    public class AuthService(AuthRepository authRepo)
    {
        private readonly AuthRepository _authRepo = authRepo;

        public async Task<UserSchema> Login(string username, string password)
        {
            return await _authRepo.Login(username, password);
        }

    }   
}
