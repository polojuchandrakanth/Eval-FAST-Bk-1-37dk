using Newtonsoft.Json;

namespace FortCode.Model.Request
{
    public class AddCountryRequest
    {
        [JsonProperty("countryid")]
        public string CountryID { get; set; }
        [JsonProperty("countryname")]
        public string CountryName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
    }
}
