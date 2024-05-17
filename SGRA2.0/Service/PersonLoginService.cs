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
    public interface IPersonLoginService
    {
        Task<List<PersonLogin>> GetAll();
        Task<PersonLogin> GetPersonLogin(int IdLoginP);
        Task<int?> GetIdByUsernameP(string usernameP);
        Task<PersonLogin> CreatePersonLogin(string UserName, string Password, int IdPerson);
        Task<PersonLogin> UpdatePersonLogin(int IdLoginP, string? UserName = null, string? Password = null, int? IdPerson = null);
        Task<PersonLogin> DeletePersonLogin(int IdLoginP);
       //
        Task<bool> Authentication(string username, string password);
        string GenerarToken(string username);
    }
    public class PersonLoginService : IPersonLoginService
    {
        public readonly IPersonLoginRepository _personLoginRepository;
        //
        private readonly IConfiguration _configuration;

        public PersonLoginService(IPersonLoginRepository personLoginRepository, IConfiguration configuration) 
        {
            _personLoginRepository = personLoginRepository;
            _configuration = configuration;
        }

        public async Task<PersonLogin> CreatePersonLogin(string UserName, string Password, int IdPerson)
        {
            return await _personLoginRepository.CreatePersonLogin(UserName, Password, IdPerson);
        }

        public async Task<PersonLogin> DeletePersonLogin(int IdLoginP)
        {
            PersonLogin personLoginToDelete = await _personLoginRepository.GetPersonLogin(IdLoginP);
            if(personLoginToDelete != null) 
            {
                throw new Exception($"El Usuario persona con el Id {IdLoginP} no existe");
            }
            personLoginToDelete.IsDelete = true;
            personLoginToDelete.Date = DateTime.Now;
            return await _personLoginRepository.DeletePersonLogin(personLoginToDelete);
        }

        public async Task<List<PersonLogin>> GetAll()
        {
            return await _personLoginRepository.GetAll();
        }

        //
        public async Task<int?> GetIdByUsernameP(string usernameP)
        {
            return await _personLoginRepository.GetIdByUsernameP(usernameP);
            throw new NotImplementedException();
        }

        public async Task<PersonLogin> GetPersonLogin(int IdLoginP)
        {
            return await _personLoginRepository.GetPersonLogin(IdLoginP);
        }

        //AUTENTICACION
        public async Task<bool> Authentication(string username, string password)
        {
            var plogin = await _personLoginRepository.AuthUser(username, password);
            if(plogin != null) 
            {
                return true;
            }
            else
            {
                return false;
            }
            //return await _personLoginRepository.AuthUser(username, password);
            throw new NotImplementedException();
        }

        //TOKEN
        public string GenerarToken(string username)
        {
            var key = _configuration.GetValue<string>("Jwt:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            //Solicitudes de Permiso
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, username));
            claims.AddClaim(new Claim(ClaimTypes.Role, "Person"));

            var credencialesToken = new SigningCredentials
                (
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(8),
                SigningCredentials = credencialesToken
            };

            //Leertoken
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        public async Task<PersonLogin> UpdatePersonLogin(int IdLoginP, string? UserName = null, string? Password = null, int? IdPerson = null)
        {
            PersonLogin newpersonLogin = await _personLoginRepository.GetPersonLogin(IdLoginP);
            if(newpersonLogin == null) 
            {
                if(UserName != null) 
                {
                    newpersonLogin.UserName = UserName;
                }
                if(Password != null) 
                {
                    newpersonLogin.Password = Password;
                }
                if (IdPerson != null)
                {
                    newpersonLogin.IdPerson = (int) IdPerson;
                }

                return await _personLoginRepository.UpdatePersonLogin(newpersonLogin);
            }
            throw new NotImplementedException();
        }
    }
    
}
