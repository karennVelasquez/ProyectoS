using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IPersonLoginRepository
    {
        Task<List<PersonLogin>> GetAll();
        Task<PersonLogin> GetPersonLogin(int id);
        Task<int?> GetIdByUsernameP(string UsernameP);
        Task<PersonLogin> CreatePersonLogin(string Username, string Password, int idPerson);
        Task<PersonLogin> UpdatePersonLogin(PersonLogin personLogin);
        Task<PersonLogin> DeletePersonLogin(PersonLogin personLogin);
        Task<PersonLogin> AuthUser(string Username);
    }
    public class PersonLoginRepositories : IPersonLoginRepository
    {
        private readonly PersonDBContext _db;

        public PersonLoginRepositories(PersonDBContext db)
        {
            _db = db;
        }

        public async Task<PersonLogin> CreatePersonLogin(string Username, string Password, int idPerson)
        {
            Person? person = _db.persons.FirstOrDefault(ut => ut.IdPerson == idPerson);
            PersonLogin newPersonLogin = new PersonLogin
            {
                Username = Username,
                Password = Password,
                IdPerson = idPerson,
                IsDelete = false,
                Date = null
            };
            _db.personLogins.AddAsync(newPersonLogin);
            _db.SaveChanges();
            return newPersonLogin;
        }

        public async Task<PersonLogin> DeletePersonLogin(PersonLogin personLogin)
        {
            _db.personLogins.Attach(personLogin);
            _db.Entry(personLogin).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return personLogin;
        }

        public async Task<List<PersonLogin>> GetAll()
        {
            return await _db.personLogins.ToListAsync();
        }

        //
        public async Task<int?> GetIdByUsernameP(string UsernameP)
        {
            var usuariop = await _db.personLogins.FirstOrDefaultAsync(u => u.Username == UsernameP);
            return usuariop?.IdLoginP;
        }

        public async Task<PersonLogin> GetPersonLogin(int id)
        {
            return await _db.personLogins.FirstOrDefaultAsync(u => u.IdLoginP == id);
        }

        //AUTENTICACION
        public async Task<PersonLogin> AuthUser(string Username)
        {
            return await _db.personLogins.FirstOrDefaultAsync(u => u.Username == Username);
        }

        public async Task<PersonLogin> UpdatePersonLogin(PersonLogin personLogin)
        {
            PersonLogin PersonLoginUpdate = await _db.personLogins.FindAsync(personLogin.IdLoginP);
            {
                PersonLoginUpdate.Username = personLogin.Username;
                PersonLoginUpdate.Password = personLogin.Password;

                await _db.SaveChangesAsync();
            }

            return PersonLoginUpdate;
        }
    }
}
