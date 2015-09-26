using Newtonsoft.Json;

namespace Numerous.Api
{
    public class User
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("userName")]
        public string UserName { get; internal set; }

        [JsonProperty("fullName")]
        public string FullName { get; internal set; }

        [JsonProperty("location")]
        public string Location { get; internal set; }

        [JsonProperty("photoURL")]
        public string PhotoUrl { get; internal set; }

        [JsonProperty("notificationsEnabled")]
        public bool NotificationsEnabled { get; internal set; }

        [JsonProperty("fbId")]
        public string FacebookId { get; internal set; }

        [JsonProperty("twId")]
        public string TwitterId { get; internal set; }

        [JsonProperty("twScreenName")]
        public string TwitterScreenName { get; internal set; }

        [JsonProperty("email")]
        public string Email { get; internal set; }

        [JsonProperty("emailVerified")]
        public bool EmailVerified { get; internal set; }

        public class Edit
        {
            [JsonProperty("userName")]
            public string UserName { get; set; }

            [JsonProperty("fullName")]
            public string FullName { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("notificationsEnabled")]
            public bool? NotificationsEnabled { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            internal Edit DefaultTo(User existingUser)
            {
                return new Edit
                {
                    UserName = UserName ?? existingUser.UserName,
                    FullName = FullName ?? existingUser.FullName,
                    Email = Email ?? existingUser.Email,
                    Location = Location ?? existingUser.Location,
                    NotificationsEnabled = NotificationsEnabled ?? existingUser.NotificationsEnabled
                };
            }
        }
    }
}