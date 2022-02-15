using DotNetNB.Security.ActionAccess;
using DotNetNB.Security.Core.Extensions;
using DotNetNB.Security.Identity.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddIdentity<IdentityUser<string>, IdentityRole<string>>()
//    .WithPermissions<IdentityUser<string>, IdentityRole<string>>();

builder.Services.AddSecurity(options =>
{
    //options.AddActionAccessControl();
    //.AddEntityAccessControl();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
