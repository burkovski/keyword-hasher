using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KeywordHasherJob
{
    internal sealed class KeywordsHashingService : IHostedService
    {
        private readonly ILogger<KeywordsHashingService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        private readonly KeywordsHashingJobSettings _keywordsHashingJobSettings;
        private readonly KeywordsHashingJob _keywordsHashingJob;

        public KeywordsHashingService(ILogger<KeywordsHashingService> logger, IHostApplicationLifetime appLifetime,
            KeywordsHashingJob keywordsHashingJob, IOptions<KeywordsHashingJobSettings> keywordsHashingJobSettings)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _keywordsHashingJob = keywordsHashingJob;
            _keywordsHashingJobSettings = keywordsHashingJobSettings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted(cancellationToken));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Action OnStarted(CancellationToken cancellationToken)
        {
            async void OnStartedAction()
            {
                try
                {
                    await _keywordsHashingJob.GenerateAndStoreHashesForKeywordsAsync(
                        _keywordsHashingJobSettings.CountriesToHash, _keywordsHashingJobSettings.BatchSize,
                        cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogCritical(exception,
                        "Unhandled exception while generating and storing hashed for keywords");
                }
                finally
                {
                    _appLifetime.StopApplication();
                }
            }

            return OnStartedAction;
        }
    }
}
