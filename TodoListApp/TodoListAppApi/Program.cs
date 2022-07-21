using Microsoft.EntityFrameworkCore;
using TodoListAppBusiness.Implementation;
using TodoListAppBusiness.Interfaces;
using TodoListAppData.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("TodoApp");

builder.Services.AddDbContext<TodoListDbContext>(builder => builder.UseSqlServer(connectionString));

builder.Services.AddScoped<TodoListDbContext>();
builder.Services.AddTransient<ITodoListService, TodoListService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
