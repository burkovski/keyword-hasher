using Jooble.SearchStringManager;
using Jooble.Text.Enumerations;

namespace Infrastructure.Services
{
    internal static class ParsedSearchStringExtensions
    {
        internal static TransformFlags MakeTransformFlags(this ParsedSearchString parsedSearchString)
        {
            var flags = GetDefaultTransformFlags();
            if (parsedSearchString.ShouldExcludeDelimiters())
                flags.AddExcludeDelimitersFlag();
            return flags;
        }

        private static TransformFlags GetDefaultTransformFlags()
        {
            return TransformFlags.CallNormalizer
                   | TransformFlags.ReplaceDiacritic;
        }

        private static bool ShouldExcludeDelimiters(this ParsedSearchString parsedSearchString)
        {
            return string.IsNullOrEmpty(parsedSearchString.SearchableAND)
                   && string.IsNullOrEmpty(parsedSearchString.SearchableNOT);
        }
    }
}
