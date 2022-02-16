namespace DotNetNB.WebApplication.Models;

public class Student
{
    
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }
    
    public string? Address { get; set; }
    
    public Teacher? Teacher { get; set; }
}