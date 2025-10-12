using System.Text;
using FootNotes.Core.Data.Communication;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using FootNotes.Crosscuting.EventSourcing;
using FootNotes.IAM.Application.Commands;
using FootNotes.IAM.Application.CommandsHandlers;
using FootNotes.IAM.Application.Interfaces;
using FootNotes.IAM.Application.Queries;
using FootNotes.IAM.Application.Queries.Interfaces;
using FootNotes.IAM.Application.Services;
using FootNotes.IAM.Data;
using FootNotes.IAM.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FootNotes.IAM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddDbContextPool<IAMContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            });

            // Mediatr, CQRS and EventSourcing
            builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
            builder.Services.AddScoped<IRequestHandler<UserRegisterCommand, CommandResponse>, UserCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<UserUpdateCommand, CommandResponse>, UserCommandHandler>();

            builder.Services.AddScoped<IEventSourcingService, EventSourcingService>();
            builder.Services.AddScoped<IEventSourcingRepository, EventSourcingRepository>(); //
            //IEventSourcingRepository

            // Services and Repositories
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Queries
            builder.Services.AddScoped<IUserQueries, UserQueries>();


            // Configurar autenticação JWT
            var jwtConfig = builder.Configuration.GetSection("Jwt");
            string? keyConfig = jwtConfig["Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            var key = Encoding.UTF8.GetBytes(keyConfig);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["Issuer"],
                    ValidAudience = jwtConfig["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<JwtService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}
