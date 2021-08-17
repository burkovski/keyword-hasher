namespace Domain.Entities
{
    public class HashedKeyword : Keyword
    {
        public long Hash { get; set; }

        public override string ToString() =>
            $"{base.ToString()} Hash={Hash}";
    }
}
