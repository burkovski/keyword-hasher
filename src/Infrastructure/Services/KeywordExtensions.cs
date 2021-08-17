using Domain.Entities;
using Jooble;
using Jooble.SearchStringManager;
using Jooble.Text;
using Jooble.Text.Entities.Tokenizer;

namespace Infrastructure.Services
{
    internal static class KeywordExtensions
    {
        internal static long GetHash(this Keyword keyword)
        {
            var keywordTokens = keyword.ExtractTokens();
            return Hash.GetHash64(keywordTokens.AsString());
        }

        private static Token[] ExtractTokens(this Keyword keyword)
        {
            var parsedSearchString = SearchStringParser.Parse(keyword.CountryCode, keyword.SearchString);
            var transformFlags = parsedSearchString.MakeTransformFlags();
            return TextProcessing.Transform(keyword.CountryCode, keyword.SearchString, transformFlags);
        }
    }
}
