
using Newtonsoft.Json;

namespace DFS.Models
{
	public class FacebookUser
    {
        public FacebookUser(string id, string token, string firstName, string lastName, string email, string imageUrl, string gender, string birthday)
        {
            Id = id;
            Token = token;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Picture = imageUrl;
            Gender = gender;
            Birthday = birthday;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Token { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Birthday { get; set; }


        public string Picture { get; set; }
    }
}


