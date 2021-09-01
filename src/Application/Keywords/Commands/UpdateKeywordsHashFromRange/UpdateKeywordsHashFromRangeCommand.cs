using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Keywords.Commands.UpdateKeywordsHashFromRange
{
    public record UpdateKeywordsHashFromRangeCommand(IEnumerable<Keyword> keywords) :
        IRequest<int>;
}
