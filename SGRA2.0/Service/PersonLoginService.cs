using SGRA2._0.Model;
using SGRA2._0.Repositories;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        Task<PersonLogin> Authentication(string username, string password);
    }
    public class PersonLoginService : IPersonLoginService
    {
        public readonly IPersonLoginRepository _personLoginRepository;

        public PersonLoginService(IPersonLoginRepository personLoginRepository) 
        {
            _personLoginRepository = personLoginRepository;
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
        public async Task<PersonLogin> Authentication(string username, string password)
        {
            return await _personLoginRepository.AuthUser(username, password);
            throw new NotImplementedException();
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
