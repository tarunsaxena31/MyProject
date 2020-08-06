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
        public int CreateEmployee(Employee emp)
        {
            if (emp.EmployeeId == 0)
            {
                Employee em = new Employee();
                //em.EmployeeId = emp.EmployeeId;
                em.FirstName = emp.FirstName;
                em.LastName = emp.LastName;                
                em.DateOfBirth = emp.DateOfBirth;
                em.Email_Id = emp.Email_Id;
                em.MobileNumber = emp.MobileNumber;
                em.Address = emp.Address;

                context.Add(em);
                context.SaveChanges();
                return em.EmployeeId;

            }
            else
            {
                var obj = context.Employees.Where(x => x.EmployeeId == emp.EmployeeId).ToList().FirstOrDefault();

                if (obj.EmployeeId > 0)
                {
                    obj.FirstName = emp.FirstName;
                    obj.LastName = emp.LastName;
                    obj.DateOfBirth = emp.DateOfBirth;                    
                    obj.Email_Id = emp.Email_Id;
                    obj.MobileNumber = emp.MobileNumber;
                    obj.Address = emp.Address;
                    context.SaveChanges();
                    return emp.EmployeeId;
                }
            }

            return 0;
                
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
