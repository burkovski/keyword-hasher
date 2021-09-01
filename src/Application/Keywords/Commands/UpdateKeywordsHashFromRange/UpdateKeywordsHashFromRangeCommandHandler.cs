using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Commands.UpdateKeywordsHashFromRange
{
    public class UpdateKeywordsHashFromRangeCommandHandler :
        IRequestHandler<UpdateKeywordsHashFromRangeCommand, int>
    {
        private readonly IKeywordHasher _keywordHasher;
        private readonly IKeywordsContext _keywordsContext;

        public UpdateKeywordsHashFromRangeCommandHandler(IKeywordsContext keywordsContext, IKeywordHasher keywordHasher)
        {
            _keywordsContext = keywordsContext;
            _keywordHasher = keywordHasher;
        }

        public async Task<int> Handle(UpdateKeywordsHashFromRangeCommand request, CancellationToken cancellationToken)
        {
            var hashedKeywords = HashKeywordsInBatch(request.keywords);
            return await SaveKeywordsBatchWithUpdatedHashesAsync(hashedKeywords, cancellationToken);
        }


        private IEnumerable<Keyword> HashKeywordsInBatch(IEnumerable<Keyword> keywords) =>
            keywords.Select(_keywordHasher.HashKeyword).AsParallel();

        private async Task<int> SaveKeywordsBatchWithUpdatedHashesAsync(IEnumerable<Keyword> hashedKeywords,
            CancellationToken cancellationToken)
        {
            _keywordsContext.Keywords.UpdateRange(hashedKeywords);
            return await _keywordsContext.SaveChangesAsync(cancellationToken);
        }
    }
}
