using System.Collections.Generic;
using Application.Common.Models;

namespace Application.Common.Extensions
{
    public static class BatchedAsyncEnumerableExtensions
    {
        public static IAsyncEnumerable<IEnumerable<T>> AsBatchedAsyncEnumerable<T>(this IAsyncEnumerable<T> items,
            int batchSize)
        {
            return new BatchedAsyncEnumerable<T>(items, batchSize);
        }
    }
}
