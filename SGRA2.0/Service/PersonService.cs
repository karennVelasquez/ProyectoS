using SGRA2._0.Model;
using SGRA2._0.Repositories;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Reflection.Metadata;

namespace SGRA2._0.Service
{
    public interface IPersonService
    {
        Task<List<Person>> GetAll();
        Task<Person> GetPerson(int IdPerson);
        Task<Person> CreatePerson(string Name, string Lastname, string Email, int IdDocumentType, int Document);
        Task<Person> UpdatePerson(int IdPerson, string? Name = null, string? Lastname = null, string? Email = null, int? IdDocumentType = null, int? Document = null);
        Task<Person> DeletePerson(int IdPerson);
     
    }
    public class PersonService : IPersonService
    {
        public readonly IPersonRepository _personRepositories;
        public PersonService(IPersonRepository personRepositories)
        {
            _personRepositories = personRepositories;
        }
        public async Task<Person> CreatePerson(string Name, string Lastname, string Email, int IdDocumentType, int Document)
        {
            return await _personRepositories.CreatePerson(Name, Lastname, Email, IdDocumentType, Document);
            //throw new NotImplementedException();
        }
        public async Task<Person> DeletePerson(int IdPerson)
        {

            // comprobar si existe
            Person personToDelete = await _personRepositories.GetPerson(IdPerson);
            if (personToDelete == null)
            {
                throw new Exception($"La persona con el Id {IdPerson} no existe");
            }
            personToDelete.IsDelete = true;
            personToDelete.Date = DateTime.Now;

            return await _personRepositories.DeletePerson(personToDelete);
        }

        public async Task<List<Person>> GetAll()
        {
            return await _personRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Person> GetPerson(int IdPerson)
        {
            return await _personRepositories.GetPerson(IdPerson);
            //throw new NotImplementedException();
        }
        public async Task<Person> UpdatePerson(int IdPerson, string? Name = null, string? Lastname = null, string? Email = null, int? IdDocumentType = null, int? Document = null)
        {
            Person newperson = await _personRepositories.GetPerson(IdPerson);
            if (newperson != null)
            {
                if (Name != null)
                {
                    newperson.Name = Name;
                }
                if (Lastname != null)
                {
                    newperson.Lastname = Lastname;
                }
                if (Email != null)
                {
                    newperson.Email = Email;
                }
                if (IdDocumentType != null)
                {
                    newperson.IdDocumentType = (int)IdDocumentType;
                }
                if (Document != null)
                {
                    newperson.Document = (int)Document;
                }
                return await _personRepositories.UpdatePerson(newperson);
            }
            throw new NotImplementedException();
        }
    }
}
