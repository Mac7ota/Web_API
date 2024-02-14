using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.ViewModel;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/Employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<EmployeeController> _logger;
    
    public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Add([FromForm]EmployeeViewModel employeeViewModel)
    {
        var filePath = Path.Combine("Storage",employeeViewModel.Photo.FileName);
        
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            employeeViewModel.Photo.CopyTo(fileStream);
        }
        
        var employee = new Employee(employeeViewModel.Name, employeeViewModel.Age, filePath);
        _employeeRepository.Add(employee);
        return Ok(employee);
    }
    
    [Authorize]
    [HttpPost]
    [Route("DownloadPhoto/{id}")]
    public IActionResult DownloadPhoto(int id)
    {
       var employee = _employeeRepository.Get(id);
       
       var dataBytes = System.IO.File.ReadAllBytes(employee.Photo);
       
       return File(dataBytes, "image/jpeg");
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Get(int pageNumber, int pageQuantity)
    {
        _logger.LogInformation("Getting employees");
        var employees = _employeeRepository.Get(pageNumber, pageQuantity);
        return Ok(employees);
    }
    
    
}