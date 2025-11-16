using FootNotes.MatchManagement.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<InsertUpcomingMatchWorker>();

var host = builder.Build();
host.Run();
