using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeDBContext context;
        public EmployeeRepository(EmployeeDBContext dbContext)
        {
            context = dbContext;
        }
        public int CreateEmployee(Employee employee)
        {
            context.Add(employee);
            context.SaveChanges();
            return employee.EmployeeId;
        }

        public Employee UpdateEmployee(Employee employee)
        {            
            context.Update(employee);
            context.SaveChanges();
            return context.Set<Employee>().ToList().Where(a => a.EmployeeId == employee.EmployeeId).FirstOrDefault();
        }

        public int DeleteEmployee(int? employeeId)
        {
            int result = 0;
            var employee = context.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefault<Employee>();

            if (employee != null)
            {
                //Delete that post                
                context.Employees.Remove(employee);
                //Commit the transaction
                result = context.SaveChanges();
            }
            return result;

        }

        public List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return context.Set<Employee>().ToList().Where(a => a.EmployeeId == employeeId).FirstOrDefault();
        }
    }
}
