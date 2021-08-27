using System.Collections.Generic;
using MediatR;

namespace Application.Keywords.Queries.GetKeywords
{
    public record GetAllKeywordsQuery(List<string> CountriesToHash, bool OmitHashed) :
        IRequest<GetAllKeywordsResult>;
}
