using DotNetNB.WebApplication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetNB.Security.ActionAccess;
using DotNetNB.Security.Core.Extensions;
using DotNetNB.Security.EntityAccess.Extensions;
using DotNetNB.Security.Identity.EntityframeworkCore.MySql;
using DotNetNB.Security.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("dotnetnb.school"),
        new MySqlServerVersion(new Version(8, 0, 27)))
);

builder.Services.AddDotNetNBSecurity(b =>
{
    b.AddActionAccessControl()
        .AddEntityAccessControl()
        .AddDbContext<ApplicationDbContext>();
})
    .AddIdentity()
    .AddEntityframeworkCore(builder.Configuration,
        builder.Configuration.GetConnectionString("dotnetnb.identity"),
        new MySqlServerVersion(new Version(8, 0, 27)));

// Adding Jwt Bearer  
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();