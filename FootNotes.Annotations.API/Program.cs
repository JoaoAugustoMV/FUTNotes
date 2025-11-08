using FootNotes.Annotations.Application.CommandHandlers;
using FootNotes.Annotations.Application.Commands.AnnotationSessionCommands;
using FootNotes.Annotations.Application.Services;
using FootNotes.Annotations.Application.Services.Impls;
using FootNotes.Annotations.Data.Context;
using FootNotes.Annotations.Data.Repositories;
using FootNotes.Annotations.Domain.Repositories;
using FootNotes.Core.Data.Communication;
using FootNotes.Core.Data.EventSourcing;
using FootNotes.Core.Messages;
using FootNotes.Crosscuting.EventSourcing;
using FootNotes.Crosscuting.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static FootNotes.Integration.MatchIntegrationService;

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

builder.Services.AddDbContextPool<AnnotationsContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

#region Grpc Clients
builder.Services.AddGrpcClient<MatchIntegrationServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcClients:MatchIntegrationService"]!);
});
#endregion

# region CQRS and EventSourcing
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

builder.Services.AddScoped<IRequestHandler<CreateNewAnnotationSessionCommand, CommandResponse>, AnnotationSessionCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AddAnnotationCommand, CommandResponse>, AnnotationSessionCommandHandler>();

builder.Services.AddScoped<IEventSourcingService, EventSourcingService>();
builder.Services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
# endregion

# region Services and Repositories
builder.Services.AddScoped<IAnnotationSessionRepository, AnnotationSessionRepository>();
builder.Services.AddScoped<IAnnotationSessionService, AnnotationSessionService>();
builder.Services.AddScoped<IAnnotationMatchService, AnnotationMatchService>();
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

app.Run();
