using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Queries.GetKeywordsWithPagination
{
    public record GetAllKeywordsQuery : IRequest<IAsyncEnumerable<Keyword>>;
}