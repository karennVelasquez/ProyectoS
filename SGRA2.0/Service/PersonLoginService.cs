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
        Task<int?> GetIdByUsernameP(string UsernameP);
        Task<PersonLogin> CreatePersonLogin(string Username, string Password, int IdPerson);
        Task<PersonLogin> UpdatePersonLogin(int IdLoginP, string? Username = null, string? Password = null, int? IdPerson = null);
        Task<PersonLogin> DeletePersonLogin(int IdLoginP);
       //
        Task<bool> Authentication(string Username, string password);
        string GenerarToken(string Username);
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

        public async Task<PersonLogin> CreatePersonLogin(string Username, string Password, int IdPerson)
        {
            Password = EncryptPassword(Password);
            return await _personLoginRepository.CreatePersonLogin(Username, Password, IdPerson);
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
        public async Task<int?> GetIdByUsernameP(string UsernameP)
        {
            return await _personLoginRepository.GetIdByUsernameP(UsernameP);
            throw new NotImplementedException();
        }

        public async Task<PersonLogin> GetPersonLogin(int IdLoginP)
        {
            return await _personLoginRepository.GetPersonLogin(IdLoginP);
        }

        private string EncryptPassword(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        //AUTENTICACION
        public async Task<bool> Authentication(string Username, string password)
        {
            var plogin = await _personLoginRepository.AuthUser(Username);
            string hashedPassword = EncryptPassword(password);
            if(plogin != null && (plogin.Password == hashedPassword)) 
            {
                return true;
            }
            else
            {
                return false;
            }
            //return await _personLoginRepository.AuthUser(Username, password);
            throw new NotImplementedException();
        }

        //TOKEN
        public string GenerarToken(string Username)
        {
            var key = _configuration.GetValue<string>("Jwt:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            //Solicitudes de Permiso
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, Username));
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

        public async Task<PersonLogin> UpdatePersonLogin(int IdLoginP, string? Username = null, string? Password = null, int? IdPerson = null)
        {
            PersonLogin newpersonLogin = await _personLoginRepository.GetPersonLogin(IdLoginP);
            if(newpersonLogin == null) 
            {
                if(Username != null) 
                {
                    newpersonLogin.Username = Username;
                }
                if(Password != null) 
                {
                    Password = EncryptPassword(Password);
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
