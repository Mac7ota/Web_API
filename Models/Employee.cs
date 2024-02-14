using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("employee")]
public class Employee
{
    [Key]
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public int Age { get; private set; }
    public string? Photo { get; private set; }

    public Employee()
    {
    }
    public Employee(string name, int age, string photo)
    {
        this.Name = name ?? throw new ArgumentException(nameof(name));
        this.Age = age;
        this.Photo = photo;
    }
    
}