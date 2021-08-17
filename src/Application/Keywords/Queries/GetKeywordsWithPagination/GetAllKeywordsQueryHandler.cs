using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Keywords.Queries.GetKeywordsWithPagination
{
    public class GetAllKeywordsQueryHandler :
        IRequestHandler<GetAllKeywordsQuery, IAsyncEnumerable<Keyword>>
    {
        private readonly IKeywordsContext _keywordsContext;

        public GetAllKeywordsQueryHandler(IKeywordsContext keywordsContext)
        {
            _keywordsContext = keywordsContext;
        }

        public async Task<IAsyncEnumerable<Keyword>>
            Handle(GetAllKeywordsQuery request, CancellationToken cancellationToken)
        {
            return _keywordsContext.Keywords
                .Distinct()
                .OrderBy(keyword => keyword.CountryCode)
                .ThenBy(keyword => keyword.SearchString)
                .AsAsyncEnumerable();
        }
    }
}
