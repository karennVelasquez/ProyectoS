using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IUserRepositories
    {
        Task<List<User>> GetAll();
        Task<User> GetUser(int id);
        //
        Task<int?> GetIdByUsername(string username);
        Task<User> CreateUser(string UserName, string Email, string Password);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);
        Task<User> AuthUser(string userName, string email);
    }
    public class UserRepositories : IUserRepositories
    {
        private readonly PersonDBContext _db;
        public UserRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<User> CreateUser(string UserName, string Email, string Password)
        {
            User newUser = new User
            {
                UserName = UserName,
                Password = Password,
                Email = Email,
                IsDelete = false,
                Date = null
            };
            _db.users.AddAsync(newUser);
            _db.SaveChanges();
            return newUser;
        }
        public async Task<User> DeleteUser(User user)
        {
            _db.users.Attach(user); 
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<List<User>> GetAll()
        {
            return await _db.users.ToListAsync();
        }
        //
        public async Task<int?> GetIdByUsername(string username)
        {
            var usuario = await _db.users.FirstOrDefaultAsync(u => u.UserName == username);
            return usuario?.IdUser;
        }
        public async Task<User> GetUser(int id)
        {
            return await _db.users.FirstOrDefaultAsync(u => u.IdUser == id);
        }

        //AUTENTICACION
        public async Task<User> AuthUser(string userName, string email)
        {
            return await _db.users.FirstOrDefaultAsync(u => u.UserName == userName && u.Email == email);
        }

        public async Task<User> UpdateUser(User user)
        {
            User UserUpdate = await _db.users.FindAsync(user.IdUser);
            {
                UserUpdate.UserName = user.UserName;
                UserUpdate.Email = user.Email;
                UserUpdate.Password = user.Password;

                await _db.SaveChangesAsync();
            }

            return UserUpdate;
        }

    }
}
