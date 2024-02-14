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

    public List<Employee> Get()
    {
        return _appDbContext.Employees.ToList();
    }
}