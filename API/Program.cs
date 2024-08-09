using API.Extensions;
using API.Interfaces;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// add indentation policy
builder.Services.AddScoped<ITokenService, TokenService>();



//add cors policy
builder.Services.AddCors();

var app = builder.Build();

// adding cors policy
app.UseCors(op => 
	op.AllowAnyHeader()
	.AllowAnyMethod()
	.WithOrigins("http://localhost:4200","https://localhost:4200")
);

// Configure authentication pipeline.
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
