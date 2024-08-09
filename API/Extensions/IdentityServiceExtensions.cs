
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
	{
		// add authentication
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			var tokenKey = config["TokenKey"] ??
			throw new Exception("Token key not found");

			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey
				(Encoding.UTF8.GetBytes(tokenKey)),
				ValidateIssuer = false,
				ValidateAudience = false
			};
		});

		return services;
	}
}
