using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class KeywordHasherService : IKeywordHasher
    {
        public HashedKeyword HashKeyword(Keyword keyword)
        {
            return new()
            {
                CountryCode = keyword.CountryCode,
                SearchString = keyword.SearchString,
                Hash = keyword.GetHash()
            };
        }
    }
}