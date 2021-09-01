using System.Collections.Generic;
using Domain.Entities;

namespace Application.Keywords.Queries.GetKeywords
{
    public record GetAllKeywordsResult(int TotalCount, IAsyncEnumerable<Keyword> Keywords);
}
