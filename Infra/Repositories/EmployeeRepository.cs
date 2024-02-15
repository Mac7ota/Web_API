using WebAPI.Domain.DTOs;
using WebAPI.Models;

namespace WebAPI.Infra;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _appDbContext = new AppDbContext();
    
    public void Add(Employee employee)
    {
        _appDbContext.Employees.Add(employee);
        _appDbContext.SaveChanges();
    }

    public List<EmployeeDTO> Get(int pageNumber, int pageQuantity)
    {
        return _appDbContext.Employees.Skip(pageNumber * pageQuantity)
            .Take(pageQuantity)
            .Select(employee => new EmployeeDTO
            {
                Id = employee.Id,
                NameEmployee = employee.Name,
                Photo = employee.Photo
            })
            .ToList();
        
    }
    
    public Employee? Get(int id)
    {
        return _appDbContext.Employees.Find(id);
    }
}