using Newtonsoft.Json;

namespace FortCode.Model.Request
{
    public class AddUserRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
