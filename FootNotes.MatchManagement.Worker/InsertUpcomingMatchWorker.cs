using FootNotes.MatchManagement.Application.Services;

namespace FootNotes.MatchManagement.Worker
{
    public class InsertUpcomingMatchWorker(ILogger<InsertUpcomingMatchWorker> logger, IMatchService matchService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await matchService.ProcessUpcommingMatch();
                //await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);// For testing purposes, set to 2 minutes
            }
        }
    }
}
