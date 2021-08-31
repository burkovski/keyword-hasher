using System.Collections.Generic;

namespace KeywordHasherJob
{
    public class KeywordsHashingJobSettings
    {
        public List<string> CountriesToHash { get; set; }
        public int BatchSize { get; set; }

        public void Deconstruct(out List<string> countriesToHash, out int batchSize)
        {
            countriesToHash = CountriesToHash;
            batchSize = BatchSize;
        }
    }
}
