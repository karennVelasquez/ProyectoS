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
        Task<Person> CreatePerson(string Name, string Lastname, string Email, int IdDocumentType, int Document);
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
        public async Task<Person> CreatePerson(string Name, string Lastname, string Email, int IdDocumentType, int Document)
        {
            //
            Person newPerson = new Person
            {
                Name = Name,
                Lastname = Lastname,
                Email = Email,
                IdDocumentType = IdDocumentType,
                Document = Document
                //
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
            //
            _db.persons.Attach(person); //Llamamos la actualizacion
            _db.Entry(person).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return person;   
        }
    }
}

