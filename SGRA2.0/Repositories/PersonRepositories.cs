using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAll();
        Task<Person> GetPerson(int id);
        Task<Person> CreatePerson(string Name, string Lastname, string Email, int IdDocumentType, int NumDocument);
        Task<Person> UpdatePerson(Person person);
        Task<Person> DeletePerson(Person person);
    }
    public class PersonRepositories : IPersonRepository
    {

        private readonly PersonDBContext _db;
        public PersonRepositories(PersonDBContext db)
        {
            _db = db;

        }
        public async Task<Person> CreatePerson(string Name, string Lastname, string Email, int idDocumentType, int NumDocument)
        {
            DocumentType? DocumentType = _db.documentTypes.FirstOrDefault(ut => ut.IdDocumentType == idDocumentType);
            Person newPerson = new Person
            {
                Name = Name,
                Lastname = Lastname,
                Email = Email,
                IdDocumentType = idDocumentType,
                NumDocument = NumDocument,
                IsDelete = false,
                Date = null
            };
            _db.persons.AddAsync(newPerson);
            _db.SaveChanges();
            return newPerson;

        }
        public async Task<Person> DeletePerson(Person person)
        {
            _db.persons.Attach(person); //Llamamos la actualizacion
            _db.Entry(person).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return person;
        }
        public async Task<List<Person>> GetAll()
        {
            return await _db.persons.ToListAsync();
        }
        public async Task<Person> GetPerson(int id)
        {
            return await _db.persons.FirstOrDefaultAsync(u => u.IdPerson == id);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            Person PersonUpdate = await _db.persons.FindAsync(person.IdPerson);
            if (PersonUpdate != null)
            {
                PersonUpdate.Name = person.Name;
                PersonUpdate.Lastname = person.Lastname;
                PersonUpdate.Email = person.Email;
                PersonUpdate.IdDocumentType = person.IdDocumentType;
                PersonUpdate.NumDocument = person.NumDocument;

                await _db.SaveChangesAsync();
            }

            return PersonUpdate;
           
        }
    }
}

