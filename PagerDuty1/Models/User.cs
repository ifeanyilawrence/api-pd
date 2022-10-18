using System.Collections.Generic;
using Newtonsoft.Json;

namespace PagerDuty1.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("contact_methods")]
        public List<ContactMethodDto> ContactMethods { get; set; }
    }

    public class ContactMethodDto
    {
        public string Id { get; set; }

        public string Self { get; set; }
    }
}