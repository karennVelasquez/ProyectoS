using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ICustomerRepositories
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetCustomer(int IdCustomer);
        Task<Customer> CreateCustomer(int IdPerson);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(Customer customer);
    }
    public class CustomerRepositories : ICustomerRepositories
    {

        private readonly PersonDBContext _db;
        public CustomerRepositories(PersonDBContext db)
        {
            _db = db;

        }
        public async Task<Customer> CreateCustomer(int IdPerson)
        {
            Customer? customer = _db.customers.FirstOrDefault(ut => ut.IdPerson == IdPerson);
            Customer newCustomer = new Customer
            {
                IdPerson = IdPerson, 
                IsDelete = false,
                Date = null
            };
            _db.customers.AddAsync(newCustomer);
            _db.SaveChanges();
            return newCustomer;
        }
        public async Task<Customer> DeleteCustomer(Customer customer)
        {
            _db.customers.Attach(customer); //Llamamos la actualizacion
            _db.Entry(customer).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return customer; ;
        }
        public async Task<List<Customer>>GetAll()
        {
            return await _db.customers.ToListAsync();
        }
        public async Task<Customer> GetCustomer(int id)
        {
            return await _db.customers.FirstOrDefaultAsync(u => u.IdCustomer == id);
        }
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
           Customer CustomerUpdate = await _db.customers.FindAsync(customer.IdCustomer);
            if (CustomerUpdate != null)
            {
               
                CustomerUpdate.IdPerson = customer.IdPerson;

                await _db.SaveChangesAsync();
            }
            //_db.customers.Attach(customer); //Llamamos la actualizacion
            //_db.Entry(customer).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return customer;
        }
    }
}
