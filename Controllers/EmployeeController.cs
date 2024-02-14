using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.ViewModel;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/Employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));    
    }
    
    [HttpPost]
    public IActionResult Add(EmployeeViewModel employeeViewModel)
    {
        var employee = new Employee(employeeViewModel.Name, employeeViewModel.Age, null);
        _employeeRepository.Add(employee);
        return Ok(employee);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var employees = _employeeRepository.Get();
        return Ok(employees);
    }
}