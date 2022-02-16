namespace DotNetNB.WebApplication.Models;

public class Teacher
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string? Address { get; set; }
}