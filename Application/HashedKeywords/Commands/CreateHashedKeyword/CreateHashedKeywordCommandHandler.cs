using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.HashedKeywords.Commands.CreateHashedKeyword
{
    public class CreateHashedKeywordCommandHandler : IRequestHandler<CreateHashedKeywordCommand, HashedKeyword>
    {
        private readonly IHashedKeywordsContext _hashedKeywordsContext;
        private readonly IKeywordHasher _keywordHasher;
        private readonly IKeywordsContext _keywordsContext;

        public CreateHashedKeywordCommandHandler(
            IKeywordsContext keywordsContext,
            IHashedKeywordsContext hashedKeywordsContext,
            IKeywordHasher keywordHasher)
        {
            _keywordsContext = keywordsContext;
            _hashedKeywordsContext = hashedKeywordsContext;
            _keywordHasher = keywordHasher;
        }

        public async Task<HashedKeyword> Handle(CreateHashedKeywordCommand request, CancellationToken cancellationToken)
        {
            var hashedKeyword = _keywordHasher.HashKeyword(request.Keyword);
            await SaveHashedKeyword(cancellationToken, hashedKeyword);
            return hashedKeyword;
        }

        private async Task SaveHashedKeyword(CancellationToken cancellationToken, HashedKeyword hashedKeyword)
        {
            _hashedKeywordsContext.Keywords.Add(hashedKeyword);
            await _hashedKeywordsContext.SaveChangesAsync(cancellationToken);
        }
    }
}