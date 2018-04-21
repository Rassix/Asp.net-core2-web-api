using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DatingApp.API.Models
{
    public class AuthorizationTokensModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken;

        [JsonProperty(PropertyName = "expiresAt")]
        public DateTime ExpiresAt;
    }
}
