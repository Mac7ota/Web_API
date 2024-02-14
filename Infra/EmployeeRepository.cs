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

    public List<Employee> Get(int pageNumber, int pageQuantity)
    {
        return _appDbContext.Employees.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        
    }
    
    public Employee? Get(int id)
    {
        return _appDbContext.Employees.Find(id);
    }
}