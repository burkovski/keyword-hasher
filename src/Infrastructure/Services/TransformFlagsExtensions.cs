using Jooble.Text.Enumerations;

namespace Infrastructure.Services
{
    internal static class TransformFlagsExtensions
    {
        internal static void AddExcludeDelimitersFlag(this ref TransformFlags flags)
        {
            flags |= TransformFlags.ExcludeDelimeters;
        }
    }
}