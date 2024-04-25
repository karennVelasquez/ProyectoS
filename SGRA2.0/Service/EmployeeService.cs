using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetEmployee(int IdEmployee);
        Task<Employee> CreateEmployee(int IdPerson, string Position);
        Task<Employee> UpdateEmployee(int IdEmployee, int? IdPerson = null, string? Position = null);
        Task<Employee> DeleteEmployee(int IdEmployee);
    }
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepositories _employeeRepositories;
        public EmployeeService(IEmployeeRepositories employeeRepositories)
        {
            _employeeRepositories = employeeRepositories;
        }
        public async Task<Employee> CreateEmployee(int IdPerson, string Position)
        {
            return await _employeeRepositories.CreateEmployee(IdPerson, Position);
            //throw new NotImplementedException();
        }

        public async Task<Employee> DeleteEmployee(int IdEmployee)
        {
            // comprobar si existe
            Employee employeeToDelete = await _employeeRepositories.GetEmployee(IdEmployee);
            if (employeeToDelete == null)
            {
                throw new Exception($"El empleado con el Id {IdEmployee} no existe");
            }

            employeeToDelete.IsDelete = true;
            employeeToDelete.Date = DateTime.Now;

            return await _employeeRepositories.DeleteEmployee(employeeToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _employeeRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployee(int IdEmployee)
        {
            return await _employeeRepositories.GetEmployee(IdEmployee);
            //throw new NotImplementedException();
        }

        public async Task<Employee> UpdateEmployee(int IdEmployee, int? IdPerson = null, string? Position = null)
        {
            Employee newemployee = await _employeeRepositories.GetEmployee(IdEmployee);
            if (newemployee != null)
            {
                if (IdPerson != null)
                {
                    newemployee.IdPerson = (int)IdPerson;
                }
                if (Position != null)
                {
                    newemployee.Position = Position;
                }
                return await _employeeRepositories.UpdateEmployee(newemployee);
            }
            throw new NotImplementedException();
        }
    }
}
