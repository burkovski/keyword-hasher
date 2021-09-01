using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IKeywordHasher
    {
        public Keyword HashKeyword(Keyword keyword);
    }
}
