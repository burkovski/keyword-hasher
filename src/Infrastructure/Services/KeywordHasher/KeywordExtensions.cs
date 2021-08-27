using Domain.Entities;
using Jooble;
using Jooble.SearchStringManager;
using Jooble.Text;
using Jooble.Text.Entities.Tokenizer;
using Jooble.Text.Enumerations;

namespace Infrastructure.Services.KeywordHasher
{
    internal static class KeywordExtensions
    {
        internal static long GenerateHash(this Keyword keyword)
        {
            var keywordTokens = keyword.ExtractTokens();
            return Hash.GetHash64(keywordTokens.AsString());
        }

        private static Token[] ExtractTokens(this Keyword keyword)
        {
            var transformFlags = keyword.MakeTransformFlags();
            return TextProcessing.Transform(keyword.CountryCode, keyword.SearchString, transformFlags);
        }

        private static TransformFlags MakeTransformFlags(this Keyword keyword)
        {
            var parsedSearchString = SearchStringParser.Parse(keyword.CountryCode, keyword.SearchString);
            return parsedSearchString.MakeTransformFlags();
        }
    }
}
