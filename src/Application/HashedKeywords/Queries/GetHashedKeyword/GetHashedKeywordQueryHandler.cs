using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.HashedKeywords.Queries.GetHashedKeyword
{
    public class GetHashedKeywordQueryHandler : IRequestHandler<GetHashedKeywordQuery, HashedKeyword>
    {
        private readonly IHashedKeywordsContext _hashedKeywordsContext;

        public GetHashedKeywordQueryHandler(IHashedKeywordsContext hashedKeywordsContext)
        {
            _hashedKeywordsContext = hashedKeywordsContext;
        }

        public Task<HashedKeyword> Handle(GetHashedKeywordQuery request, CancellationToken cancellationToken)
        {
            return _hashedKeywordsContext.Keywords
                .FirstOrDefaultAsync(keyword =>
                        keyword.CountryCode == request.Keyword.CountryCode
                        && keyword.SearchString == request.Keyword.SearchString,
                    cancellationToken);
        }
    }
}
