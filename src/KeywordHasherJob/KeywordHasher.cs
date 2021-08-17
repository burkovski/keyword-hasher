using System.Threading;
using System.Threading.Tasks;
using Application.HashedKeywords.Commands.CreateHashedKeyword;
using Application.HashedKeywords.Queries.GetHashedKeyword;
using Application.Keywords.Queries.GetKeywordsWithPagination;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KeywordHasherJob
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
                if (await IsKeywordAlreadyHashedAsync(keyword, cancellationToken))
                    continue;

                var hashedKeyword = await HashAndStoreKeywordAsync(keyword, cancellationToken);

                _logger.LogInformation("HashedKeyword: {HashedKeyword}", hashedKeyword);
            }
        }

        private async Task<bool> IsKeywordAlreadyHashedAsync(Keyword keyword, CancellationToken cancellationToken)
        {
            var getHashedKeywordQuery = new GetHashedKeywordQuery(keyword);
            var alreadyHashedKeyword = await _mediatr.Send(getHashedKeywordQuery, cancellationToken);
            return alreadyHashedKeyword != null;
        }

        private async Task<HashedKeyword> HashAndStoreKeywordAsync(Keyword keyword, CancellationToken cancellationToken)
        {
            var createHashedKeywordCommand = new CreateHashedKeywordCommand(keyword);
            return await _mediatr.Send(createHashedKeywordCommand, cancellationToken);
        }
    }
}
