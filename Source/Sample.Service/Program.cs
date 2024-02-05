using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.DTO;
using Sample.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

string connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PersonsContext>( options =>
{
    options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("Sample.Service"));
});

builder.Services.AddScoped<IBackendService, BackendService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/people", (IBackendService service) =>
{
    return service.GetAllPersonsAsync();
});

app.MapPost("/api/people", (IBackendService service, PersonDTO person) =>
{
    return service.AddPersonAsync(person);
});

app.MapPut("/api/people/{id}", (IBackendService service,int id, PersonDTO personDTO) =>
{
    return service.UpdatePersonAsync(id, personDTO);
});

app.MapDelete("/api/people/{id}", (IBackendService service, int id) =>
{
    return service.DeletePersonAsync(id);
});

app.Run();