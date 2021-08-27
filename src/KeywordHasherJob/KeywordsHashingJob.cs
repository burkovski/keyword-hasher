using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Keywords.Commands.UpdateKeywordsHashFromRange;
using Application.Keywords.Queries.GetKeywords;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KeywordHasherJob
{
    internal class KeywordsHashingJob
    {
        private readonly ILogger<KeywordsHashingJob> _logger;
        private readonly ISender _mediatr;

        public KeywordsHashingJob(ILogger<KeywordsHashingJob> logger, ISender mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        public async Task GenerateAndStoreHashesForKeywordsAsync(List<string> countriesToHash, int batchSize,
            CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (totalCount, keywords) = await GetAllKeywordsAsync(countriesToHash, cancellationToken);
            var updatedCount = 0;

            var keywordsBatches = keywords.AsBatchedAsyncEnumerable(batchSize).WithCancellation(cancellationToken);
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
