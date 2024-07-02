using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookies.Core.Selfies.infrastructures.Configuration;

namespace SelfieAWookie.API.UI.ExtentionMethods
{
    // gestion des cors et jwt
    public static class SecurityMethods
    {
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public static void AddCusumCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors( option =>
            {
                option.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    // ou par IP en dur ou via appsetting.json
                    //builder.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
                    // ou via le fichier de conf app settigs 
                    //builder.WithOrigins(configuration["Cors:Origin"]).AllowAnyHeader().AllowAnyMethod();

                    
                });
            });

        }

        public static void addCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // optonel pour recuperer le Jwt key par option et par injection
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }

        public static void AddCustomJWT(this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOption secutityOption = new SecurityOption();
            configuration.GetSection("Jwt").Bind(secutityOption);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( options =>
            {
                //string cle = configuration["Jwt:Key"]; recuperation direct des fichiers settigns ou secret si on ne passe pas par configuration -> SecutityOption
                string cle = secutityOption.Key;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(cle)) ,

                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor =false,
                    ValidateLifetime = true,

                };
            });
        }


    }
}
