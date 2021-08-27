using System.Text;

namespace Domain.Entities
{
    public record Keyword(int Id, string CountryCode, string SearchString, long? Hash)
    {
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Id={0} CountryCode={1} SearchString={2}", Id, CountryCode, SearchString);
            if (Hash.HasValue)
                stringBuilder.AppendFormat(" Hash={0}", Hash.Value);
            return stringBuilder.ToString();
        }
    }
}
