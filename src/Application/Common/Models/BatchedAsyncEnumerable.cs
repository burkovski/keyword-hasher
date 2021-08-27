using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class BatchedAsyncEnumerable<T> : IAsyncEnumerable<IEnumerable<T>>
    {
        private readonly int _batchSize;
        private readonly IAsyncEnumerable<T> _items;

        public BatchedAsyncEnumerable(IAsyncEnumerable<T> items, int batchSize)
        {
            _items = items;
            _batchSize = batchSize;
        }

        public async IAsyncEnumerator<IEnumerable<T>> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            var batch = new List<T>();
            await foreach (var item in _items.WithCancellation(cancellationToken))
            {
                batch.Add(item);

                if (batch.Count != _batchSize) continue;

                yield return batch;
                batch = new List<T>();
            }

            if (batch.Count > 0)
                yield return batch;
        }
    }
}
