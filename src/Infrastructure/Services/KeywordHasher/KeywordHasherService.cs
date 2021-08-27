using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services.KeywordHasher
{
    public class KeywordHasherService : IKeywordHasher
    {
        public Keyword HashKeyword(Keyword keyword)
        {
            var hash = keyword.GenerateHash();
            return keyword with { Hash = hash };
        }
    }
}
