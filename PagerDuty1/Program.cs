using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using PagerDuty1.Models;

namespace PagerDuty1
{
    class Program
    {
        private HttpClient client;
        
        private const string token = "y_NbAkKc66ryYTWUXYEu";
        private static Dictionary<string, User> users = new Dictionary<string, User>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var userResponse = GetUsers();

            foreach (var user in userResponse.Users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}");
            }

            var input = Console.ReadLine();

            GetUserDetail(input, users[input].ContactMethods.First().Id);
        }

        public static GetUserResult GetUsers()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.pagerduty.com");
            client.DefaultRequestHeaders.Add("Authorization", $"Token token={token}");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.pagerduty+json;version=2");

            var response = client.GetAsync("/users?include[]=contact_methods").GetAwaiter().GetResult();

            var deserializedResponse = JsonConvert.DeserializeObject<GetUserResult>(response.Content.ReadAsStringAsync().Result);

            deserializedResponse?.Users.ForEach(s =>
            {
                users.Add(s.Id, s);
            });

            return deserializedResponse;
        }

        public static void GetUserDetail(
            string userId, 
            string contactMethodId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.pagerduty.com");
            client.DefaultRequestHeaders.Add("Authorization", $"Token token={token}");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.pagerduty+json;version=2");

            var response = client.GetAsync($"/users/{userId}/contact_methods/{contactMethodId}").GetAwaiter().GetResult();

            var var = response.Content.ReadAsStringAsync().Result;
        }
    }
}