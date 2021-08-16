using Domain.Entities;
using MediatR;

namespace Application.HashedKeywords.Commands.CreateHashedKeyword
{
    public record CreateHashedKeywordCommand(Keyword Keyword) :
        IRequest<HashedKeyword>;
}