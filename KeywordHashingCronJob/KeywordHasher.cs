using System;
using System.Threading;
using System.Threading.Tasks;
using Application.HashedKeywords.Commands.CreateHashedKeyword;
using Application.HashedKeywords.Queries.GetHashedKeyword;
using Application.Keywords.Queries.GetKeywordsWithPagination;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KeywordHashingCronJob
{
    internal class KeywordHasher
    {
        private readonly ILogger _logger;
        private readonly ISender _mediatr;

        public KeywordHasher(ILogger<KeywordHasher> logger, ISender mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        internal async Task GenerateAndStoreHashesForKeywordsAsync(CancellationToken cancellationToken)
        {
            var getAllKeywordsQuery = new GetAllKeywordsQuery();
            var keywords = await _mediatr.Send(getAllKeywordsQuery, cancellationToken);

            await foreach (var keyword in keywords.WithCancellation(cancellationToken))
            {
                var getHashedKeywordQuery = new GetHashedKeywordQuery(keyword);
                var alreadyHashedKeyword = await _mediatr.Send(getHashedKeywordQuery, cancellationToken);
                if (alreadyHashedKeyword != null)
                    continue;

                Console.WriteLine($"{keyword.CountryCode}, {keyword.SearchString}");

                var createHashedKeywordCommand = new CreateHashedKeywordCommand(keyword);
                await _mediatr.Send(createHashedKeywordCommand, cancellationToken);
            }
        }
    }
}
