using Microsoft.EntityFrameworkCore;
using SingleRepoPokemonApi.Data;
using SingleRepoPokemonApi.EndpointAPI;
using SingleRepoPokemonApi.Repositories;
using SingleRepoPokemonApi.Repositories.IRepo;
using SingleRepoPokemonApi.Service;
using SingleRepoPokemonApi.Service.IService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Service 
builder.Services.AddScoped<IPokemonService, PokemonService>();

//Add Repo
builder.Services.AddTransient(typeof(IRepo<>), typeof(Repo<>));
builder.Services.AddTransient<IPokemonRepo, PokemonRepo>();

//Add EF Db
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("DefaultConnection Not Found");
builder.Services.AddDbContextFactory<AppDbContext>(o => o.UseSqlServer(connectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.PokemonApi();

app.Run();


