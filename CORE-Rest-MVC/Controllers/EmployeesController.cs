using BlazorProject.Server.Models;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository EmployeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

      

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            try
            {
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // or
                    var iden= identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    return Ok(await EmployeeRepository.GetEmployees());
                }
                else 
                {
                    return Unauthorized("Token not valid");
                }
                return BadRequest("Token not valid");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeesById(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // or
                    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await EmployeeRepository.GetEmployee(id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return result;
                    
                }
                else
                {
                    return Unauthorized("Token not valid");
                }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployees( Employee employee)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // or
                    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (employee == null)
                    {
                        return BadRequest();
                    }

                    var emp = await EmployeeRepository.GetEmployeeByEmail(employee.Email);
                    if (emp != null)
                    {
                        ModelState.AddModelError("Email", "Email already in use");
                        return BadRequest(ModelState);
                    }
                    var result = await EmployeeRepository.AddEmployee(employee);

                    return CreatedAtAction(nameof(GetEmployeesById), new { id = result.EmployeeId }, result);

                }
                else
                {
                    return Unauthorized("Token not valid");
                }

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    e + "Error creating new employee record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployees(int id, Employee employee)
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //if (identity != null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                //    // or
                //    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (id != employee.EmployeeId)
                    {
                        return BadRequest("Employee id mismatch");
                    }

                    var empUpdate = await EmployeeRepository.GetEmployee(id);
                    if (empUpdate == null)
                    {
                        return NotFound($"Employee with id {id} not found");
                    }
                    return await EmployeeRepository.UpdateEmployee(employee);
                //}
                //else
                //{
                //    return Unauthorized("Token not valid");
                //}



            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEmployees(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // or
                    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var empDelete = await EmployeeRepository.GetEmployee(id);
                    if (empDelete == null)
                    {
                        return NotFound($"Employee with id {id} not found");
                    }
                    await EmployeeRepository.DeleteEmployee(id);
                    return Ok($"Employee with id {id} is deleted");
                }
                else
                {
                    return Unauthorized("Token not valid");
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search (string name, Gender? gender)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // or
                    var iden = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var result = await EmployeeRepository.Search(name, gender);
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    return NotFound();
                }
                else
                {
                    return Unauthorized("Token not valid");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }

    }
}
