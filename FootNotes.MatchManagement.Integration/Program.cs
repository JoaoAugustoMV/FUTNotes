using FootNotes.Core.Data.Communication;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using FootNotes.Crosscuting.EventSourcing;
using FootNotes.Crosscuting.Logging;
using FootNotes.MatchManagement.Application.Services;
using FootNotes.MatchManagement.Application.Services.Impls;
using FootNotes.MatchManagement.Data.Context;
using FootNotes.MatchManagement.Data.Repositories;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Integration.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
# region Logging
LoggingConfiguration.Configure(builder.Configuration);
builder.Host.UseSerilog();
# endregion


builder.Services.AddDbContextPool<MatchContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

# region CQRS and EventSourcing
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

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
app.MapGrpcService<MatchIntegrationGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
