using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Keywords.Commands.UpdateKeywordsHashFromRange;
using Application.Keywords.Queries.GetKeywords;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KeywordHasherJob
{
    internal class KeywordsHashingJob
    {
        private readonly ILogger<KeywordsHashingJob> _logger;
        private readonly ISender _mediatr;
        private readonly KeywordsHashingJobSettings _keywordsHashingJobSettings;

        public KeywordsHashingJob(ILogger<KeywordsHashingJob> logger, ISender mediatr,
            IOptions<KeywordsHashingJobSettings> keywordsHashingJobSettings)
        {
            _logger = logger;
            _mediatr = mediatr;
            _keywordsHashingJobSettings = keywordsHashingJobSettings.Value;
        }

        public async Task GenerateAndStoreHashesForKeywordsAsync(CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (countriesToHash, batchSize) = _keywordsHashingJobSettings;

            var (totalCount, keywords) = await GetAllKeywordsAsync(countriesToHash, cancellationToken);
            var keywordsBatches = keywords.Buffer(batchSize).WithCancellation(cancellationToken);

            var updatedCount = 0;
            await foreach (var keywordsBatch in keywordsBatches)
                updatedCount += await UpdateHashesForKeywordsBatchAsync(keywordsBatch, cancellationToken);

            _logger.LogInformation("Updated: {UpdatedCount}/{TotalCount} keywords", totalCount, updatedCount);

            stopwatch.Stop();
            _logger.LogInformation("Elapsed time: {ElapsedTime}", stopwatch.Elapsed);
        }

        private async Task<int> UpdateHashesForKeywordsBatchAsync(IEnumerable<Keyword> keywords,
            CancellationToken cancellationToken)
        {
            var updateKeywordsHashFromRangeCommand = new UpdateKeywordsHashFromRangeCommand(keywords);
            var updatedCount = await _mediatr.Send(updateKeywordsHashFromRangeCommand, cancellationToken);
            return updatedCount;
        }

        private async Task<GetAllKeywordsResult> GetAllKeywordsAsync(List<string> countriesToHash,
            CancellationToken cancellationToken)
        {
            var getAllKeywordsQuery = new GetAllKeywordsQuery(countriesToHash, true);
            return await _mediatr.Send(getAllKeywordsQuery, cancellationToken);
        }
    }
}
