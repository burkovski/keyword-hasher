using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Keywords.Queries.GetKeywords
{
    public class GetAllKeywordsQueryHandler :
        IRequestHandler<GetAllKeywordsQuery, GetAllKeywordsResult>
    {
        private readonly IKeywordsContext _keywordsContext;

        public GetAllKeywordsQueryHandler(IKeywordsContext keywordsContext)
        {
            _keywordsContext = keywordsContext;
        }

        public async Task<GetAllKeywordsResult> Handle(GetAllKeywordsQuery request,
            CancellationToken cancellationToken)
        {
            var (countriesToHash, omitHashed) = request;

            var keywords = _keywordsContext.Keywords
                .Where(keyword => countriesToHash.Contains(keyword.CountryCode))
                .OrderBy(keyword => keyword.CountryCode)
                .ThenBy(keyword => keyword.SearchString)
                .AsNoTracking();

            if (omitHashed)
                keywords = keywords.Where(keyword => !keyword.Hash.HasValue);

            var count = await keywords.CountAsync(cancellationToken);

            return new GetAllKeywordsResult(count, keywords.AsAsyncEnumerable());
        }
    }
}
