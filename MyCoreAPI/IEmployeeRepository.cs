using System.Collections.Generic;

namespace MyCoreAPI
{
    public interface IEmployeeRepository
    {
        public int CreateEmployee(Employee employee);
        public Employee UpdateEmployee(Employee employee);
        public int DeleteEmployee(int? employeeId);
        public List<Employee> GetEmployees();
        public Employee GetEmployeeById(int employeeId);
    }
}