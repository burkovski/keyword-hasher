using Domain.Entities;
using MediatR;

namespace Application.HashedKeywords.Queries.GetHashedKeyword
{
    public record GetHashedKeywordQuery(Keyword Keyword) : IRequest<HashedKeyword>;
}