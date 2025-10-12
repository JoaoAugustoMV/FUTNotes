using FootNotes.Core.Data.Communication;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using FootNotes.Crosscuting.EventSourcing;
using FootNotes.Crosscuting.Logging;
using FootNotes.MatchManagement.Application.CommandHandlers;
using FootNotes.MatchManagement.Application.Commands;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Services;
using FootNotes.MatchManagement.Application.Services.Impls;
using FootNotes.MatchManagement.Data.Context;
using FootNotes.MatchManagement.Data.Repositories;
using FootNotes.MatchManagement.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


# region Logging
LoggingConfiguration.Configure(builder.Configuration);
builder.Host.UseSerilog();
# endregion

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<MatchContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

# region CQRS and EventSourcing
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

builder.Services.AddScoped<IRequestHandler<CreateTeamManuallyCommand, CommandResponse>, TeamCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateMatchManuallyCommand, CommandResponse>, MatchCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateMatchStatusCommand, CommandResponse>, MatchCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateScoreMatchCommand, CommandResponse>, MatchCommandHandler>();

builder.Services.AddScoped<IEventSourcingService, EventSourcingService>();
builder.Services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
# endregion


# region Services and Repositories
builder.Services.AddScoped<IMatchService, MatchService>();

builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
#endregion

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

app.UseMiddleware<CorrelationIdMiddleware>();

app.Run();
