using Newtonsoft.Json;

namespace FortCode.Model.Request
{
    public class AddCountryRequest
    {
        [JsonProperty("countryname")]
        public string CountryName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
    }
}
