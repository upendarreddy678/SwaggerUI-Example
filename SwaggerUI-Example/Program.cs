using DataSrv.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DataSrv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    if (File.Exists(xmlFile))
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DomainSrv.xml"));
    }
    //if (File.Exists("DomainSrv.xml"))
    //{
    //    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DomainSrv.xml"));
    //}
});
builder.Services.DataDependencyinjection();

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
