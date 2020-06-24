using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        //[Route("GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            var rng = new Random();
                        
            return _employeeRepository.GetEmployees();                        
        }

        [HttpGet]
        [Route("{employeeId}")]
        public Employee GetEmployee(int employeeId)
        {       

            return _employeeRepository.GetEmployeeById(employeeId);

        }

        //http://localhost:12817/Employee/CreateEmployee/Pratyush/Saxena/07-11-2019/pratyush.saxena@gmail.com/7350312048/wakad

        [HttpPost]
        //[Route("CreateEmployee/{firstName}/{lastName}/{dateOfBirth}/{email_Id}/{mobileNumber}/{address}")]
        //public int CreateEmployee([FromBody] Employee employee)
        //public int CreateEmployee(string firstName, string lastName, DateTime dateOfBirth, string email_Id, string mobileNumber, string address)
        [Route("CreateEmployee")]
        public int CreateEmployee([FromBody]Employee employee)
        {
            //Employee employee = new Employee
            //{
            //    FirstName = firstName,
            //    LastName = lastName,
            //    DateOfBirth = dateOfBirth,
            //    Email_Id = email_Id,
            //    MobileNumber = mobileNumber,
            //    Address = address
            //};

            int employeeId =  _employeeRepository.CreateEmployee(employee);

            return employeeId;
            //return true;
        }

        [HttpPost]
        [Route("UpdateEmployee")]
        public Employee UpdateEmployee([FromBody]Employee employee)
        {            
            Employee employeeobj = _employeeRepository.UpdateEmployee(employee);

            return employeeobj;
            //return true;
        }

        [HttpPost]
        [Route("DeleteEmployee/{employeeId}")]
        public IActionResult DeleteEmployee(int? employeeId)
        {
            int result = 0;
            if (employeeId == null)
            {
                return BadRequest();
            }

            try
            {
                result = _employeeRepository.DeleteEmployee(employeeId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
