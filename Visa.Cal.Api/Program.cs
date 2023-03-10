using Visa.Cal.Abstraction.Data;
using Visa.Cal.Abstraction.Domain;
using Visa.Cal.Data;

//const string connectionString = "Server=127.0.0.1\\mssqllocldb,1433;Database=ProfileContextTest;User=sa;Password=Abgo!Den!1977";

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProfileContext");

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryGeneric<SystemPortfolio>, RepositoryGeneric<SystemPortfolio>>
    ( _ =>
        {
            var dbContext = ProfileContext.Create(connectionString);
            dbContext.Database.EnsureCreated();
            return new RepositoryGeneric<SystemPortfolio>(dbContext);
        });



var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
     app.UseSwagger();
     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


