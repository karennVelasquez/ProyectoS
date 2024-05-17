using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGRA2._0.Model;
using SGRA2._0.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SGRA2._0.Service
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetUser(int IdUser);
        //
        Task<int?> GetIdByUsername(string username);
        Task<User> CreateUser(string UserName, string Email, string Password);
        Task<User> UpdateUser(int IdUser, string? UserName = null, string? Email=null, string? Password = null);
        Task<User> DeleteUser(int IdUser);
        //
        Task<User> Authentication(string userName, string email, string password);
        string EncryptPassword (string password);
        string GenerarToken(string username);
    }
    public class UserService : IUserService
    {
        public readonly IUserRepositories _userRepositories;
        //
        private readonly IConfiguration _configuration;
        public UserService(IUserRepositories userRepositories, IConfiguration configuration)
        {
            _userRepositories = userRepositories;
            //
            _configuration = configuration;
        }
        public async Task<User> CreateUser(string UserName, string Email, string Password)
        {
            return await _userRepositories.CreateUser(UserName, Email, Password);
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

        //
        public async Task<int?> GetIdByUsername(string username)
        {
            return await _userRepositories.GetIdByUsername(username);
            throw new NotImplementedException();
        }


        public async Task<User> GetUser(int IdUser)
        {
            return await _userRepositories.GetUser(IdUser);
            //throw new NotImplementedException();
        }
        //ENCRIP CONTRASEÑA 
        public string EncryptPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for(int i =0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        //AUTENTICACION
        public async Task<User> Authentication(string userName, string email, string password)
        {
            //var login = await _userRepositories.AuthUser(userName, email, password);

            return await _userRepositories.AuthUser(userName, email, password);
            // string hashedPassword = EncryptPassword(password);
            // if (login == null && (login.Password == hashedPassword))
            // return false;
            throw new NotImplementedException();
        }


        //TOKEN
        public string GenerarToken(string username)
        {
            var key = _configuration.GetValue<string>("Jwt:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, username));
            claims.AddClaim(new Claim(ClaimTypes.Role, "User"));

            var credencialesToken = new SigningCredentials
                (
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }
        public async Task<User> UpdateUser(int IdUser, string? UserName = null, string? Email = null, string? Password = null)
        {
            User newuser = await _userRepositories.GetUser(IdUser);
            if (newuser != null)
            {
                if (UserName != null)
                {
                    newuser.UserName = UserName;
                }
                if (Email != null)
                {
                    newuser.Email = Email;
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
