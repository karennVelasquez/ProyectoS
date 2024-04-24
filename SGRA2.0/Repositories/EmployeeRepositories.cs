using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetEmployee(int IdEmployee);
        Task<Employee> CreateEmployee(int IdPerson, string Position);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(Employee employee);
    }
    public class EmployeeRepositories : IEmployeeRepository
    {
        private readonly PersonDBContext _db;
        public EmployeeRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Employee> CreateEmployee(int IdPerson, string Position)
        {
            //
            Employee newEmployee = new Employee
            {
                IdPerson = IdPerson,
                Position = Position
                //
            };
            _db.employees.AddAsync(newEmployee);
            _db.SaveChanges();
            return newEmployee;
        }
        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            _db.employees.Attach(employee); //Llamamos la actualizacion
            _db.Entry(employee).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return employee;
        }
        public async Task<List<Employee>> GetAll()
        {
            return await _db.employees.ToListAsync();
        }
        public async Task<Employee> GetEmployee(int id)
        {
            return await _db.employees.FirstOrDefaultAsync(u => u.IdEmployee == id);
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            Employee EmployeeUpdate = await _db.employees.FindAsync(employee.IdEmployee);
            if (EmployeeUpdate != null)
            {
                EmployeeUpdate.IdPerson = employee.IdPerson;
                EmployeeUpdate.Position = employee.Position;

                await _db.SaveChangesAsync();
            }
            //_db.employees.Attach(employee); //Llamamos la actualizacion
            //_db.Entry(employee).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return employee;
        }
    }
}
