using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IKeywordHasher
    {
        public HashedKeyword HashKeyword(Keyword keyword);
    }
}
