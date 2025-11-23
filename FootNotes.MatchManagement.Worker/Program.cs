using FootNotes.Core.Data.Communication;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using FootNotes.Crosscuting.EventSourcing;
using FootNotes.Crosscuting.Logging;
using FootNotes.MatchManagement.Adapters.MatchProviders.Dojo;
using FootNotes.MatchManagement.Application.CommandHandlers;
using FootNotes.MatchManagement.Application.Commands;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Application.Services;
using FootNotes.MatchManagement.Application.Services.Impls;
using FootNotes.MatchManagement.Data.Context;
using FootNotes.MatchManagement.Data.Repositories;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Worker;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<InsertUpcomingMatchWorker>();

# region Logging
LoggingConfiguration.Configure(builder.Configuration);
builder.Services.AddSerilog();
# endregion


builder.Services.AddHttpClient();
builder.Services.AddDbContextPool<MatchContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

# region CQRS and EventSourcing
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

builder.Services.AddScoped<IRequestHandler<InsertUpcomingMatchsCommand, CommandResponse>, MatchCommandHandler>();

builder.Services.AddScoped<IEventSourcingService, EventSourcingService>();
builder.Services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
# endregion

#region Repositories and Services
builder.Services.AddTransient<ITeamRepository, TeamRepository>();
builder.Services.AddTransient<IMatchRepository, MatchRepository>();

builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IMatchProvider, DojoMatchProvider>();
builder.Services.Configure<DojoConfigs>(
    builder.Configuration.GetSection("DojoConfigs")
);

#endregion
var host = builder.Build();
host.Run();
