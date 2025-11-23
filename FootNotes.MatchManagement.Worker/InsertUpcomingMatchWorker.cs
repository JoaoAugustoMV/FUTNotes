using FootNotes.MatchManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FootNotes.MatchManagement.Worker
{
    public class InsertUpcomingMatchWorker(ILogger<InsertUpcomingMatchWorker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {                    
                    using IServiceScope scope = serviceScopeFactory.CreateScope();

                    IMatchService matchService = scope.ServiceProvider.GetRequiredService<IMatchService>();

                    await matchService.ProcessUpcommingMatch();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while processing upcoming matches.");
                }
                
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);                
            }
        }
    }
}
