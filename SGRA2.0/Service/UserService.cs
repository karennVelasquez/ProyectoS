using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetUser(int IdUser);
        Task<User> CreateUser(string UserName, string Password);
        Task<User> UpdateUser(int IdUser, string? UserName = null, string? Password = null);
        Task<User> DeleteUser(int IdUser);
    }
    public class UserService : IUserService
    {
        public readonly UserRepositories _userRepositories;
        public UserService(UserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }
        public async Task<User> CreateUser(string UserName, string Password)
        {
            return await _userRepositories.CreateUser(UserName, Password);
            //throw new NotImplementedException();
        }

        public async Task<User> DeleteUser(int IdUser)
        {
            // comprobar si existe
            User userToDelete = await _userRepositories.GetUser(IdUser);
            if (userToDelete == null)
            {
                throw new Exception($"El usuario con el Id {IdUser} no existe");
            }
            userToDelete.IsDelete = true;
            userToDelete.Date = DateTime.Now;
            return await _userRepositories.DeleteUser(userToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<User> GetUser(int IdUser)
        {
            return await _userRepositories.GetUser(IdUser);
            //throw new NotImplementedException();
        }

        public async Task<User> UpdateUser(int IdUser, string? UserName = null, string? Password = null)
        {
            User newuser = await _userRepositories.GetUser(IdUser);
            if (newuser != null)
            {
                if (UserName != null)
                {
                    newuser.UserName = UserName;
                }
                if (Password != null)
                {
                    newuser.Password = Password;
                }
            }
            return await _userRepositories.UpdateUser(newuser);
            throw new NotImplementedException();
        }
    }
}
