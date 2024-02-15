using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.DTOs;
using WebAPI.Models;
using WebAPI.ViewModel;

namespace WebAPI.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/Employee")]
[ApiVersion("2.0")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<EmployeeController> _logger;
    private readonly IMapper _mapper;
    
    public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMapper mapper)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
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
    
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public IActionResult Search(int id)
    {
        _logger.LogInformation("Getting employees");
        var employees = _employeeRepository.Get(id);
        var employessDTO = _mapper.Map<EmployeeDTO>(employees);
        return Ok(employessDTO);
    }
    
}