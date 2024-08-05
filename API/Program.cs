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

//add cors policy
builder.Services.AddCors();

var app = builder.Build();

// adding cors policy
app.UseCors(op => 
    op.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200")
);


// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
