
using Archieve.Domain.Extensions;
using Archieve.Domain.Helpers.Authorizations;
using Archieve.Domain.Interfaces;
using Archieve.Domain.Services;
using Archieve.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ArchieveContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));

builder.Services.AddScoped<IBookService, BookServices>();
builder.Services.AddScoped<IAccountService, AccountServices>();
builder.Services.AddScoped<IRoleService, RolesServices>();
builder.Services.ConfigureAuthorizationServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<JwtMiddleWare>();
app.UseMiddleware<PermissionsMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();



app.MapControllers();

app.Run();
