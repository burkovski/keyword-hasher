namespace Domain.Entities
{
    public class Keyword
    {
        public string CountryCode { get; set; }

        public string SearchString { get; set; }

        public override string ToString() =>
            $"CountryCode={CountryCode} SearchString={SearchString}";
    }
}
