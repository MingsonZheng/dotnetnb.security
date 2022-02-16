using DotNetNB.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetNB.WebApplication.Controllers;


[Controller]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public StudentController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Student(string name)
    {
        return Ok(await _dbContext.Students.AsQueryable().ToListAsync());
    }
    
    [Route("{name}")]
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute]string name)
    {
        _dbContext.Students.Add(new Student() {Name = name});
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
    [Route("{name}")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute]string name)
    {
        var student = await _dbContext.Students.SingleOrDefaultAsync(s => s.Name == name);
        _dbContext.Remove(student);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
    [Route("addAge/{name}")]
    [HttpPut]
    public async Task<IActionResult> AddAge([FromRoute] string name)
    {
        var student = await _dbContext.Students.SingleOrDefaultAsync(s => s.Name == name);
        student.Age += 1;
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
}