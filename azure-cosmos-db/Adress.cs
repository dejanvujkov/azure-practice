using Newtonsoft.Json;

namespace azure_cosmos_db
{
    public class Adress
    {
        public Adress(string city, int postalCode)
        {
            City = city;
            PostalCode = postalCode;
        }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postalCode")]
        public int PostalCode { get; set; }
    }
}