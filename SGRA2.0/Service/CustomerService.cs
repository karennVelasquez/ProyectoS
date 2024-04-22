using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetCustomer(int IdCustomer);
        Task<Customer> CreateCustomer(int IdPerson);
        Task<Customer> UpdateCustomer(int IdCustomer, int? IdPerson = null);
        Task<Customer> DeleteCustomer(int IdCustomer);
    }
    public class CustomerService : ICustomerService
    {
        public readonly CustomerRepositories _customerRepositories;
        public CustomerService(CustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }
        public async Task<Customer> CreateCustomer(int IdPerson)
        {
            return await _customerRepositories.CreateCustomer(IdPerson);
            //throw new NotImplementedException();
        }

        public async Task<Customer> DeleteCustomer(int IdCustomer)
        {
            // comprobar si existe
            Customer customerToDelete = await _customerRepositories.GetCustomer(IdCustomer);
            if (customerToDelete == null)
            {
                throw new Exception($"El cliente con el Id {IdCustomer} no existe");
            }

            customerToDelete.IsDeleted = true;
            customerToDelete.Date = DateTime.Now;

            return await _customerRepositories.DeleteCustomer(customerToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _customerRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomer(int IdCustomer)
        {
            return await _customerRepositories.GetCustomer(IdCustomer);
            //throw new NotImplementedException();
        }
        public async Task<Customer> UpdateCustomer(int IdCustomer, int? IdPerson = null)
        {
            Customer newcustomer = await _customerRepositories.GetCustomer(IdCustomer);
            if (newcustomer != null)
            {
                if (IdPerson != null)
                {
                    newcustomer.IdPerson = (int)IdPerson;
                }
                return await _customerRepositories.UpdateCustomer(newcustomer);
            }
            throw new NotImplementedException();
        }
    }
}
