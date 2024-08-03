using API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// adding db context to the services container
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration
    .GetConnectionString("DefaultConnection"));
});


var app = builder.Build();




app.MapControllers();

app.Run();
