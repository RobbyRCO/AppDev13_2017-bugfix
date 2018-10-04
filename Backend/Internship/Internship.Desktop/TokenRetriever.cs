using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Internship.Desktop
{
    public class TokenRetriever
    {
        public const string baseUrl = "http://localhost:57280";
        public async Task<string> RetrieveToken(string username, string password, string apirUri)
        {

            using (var client = SetUpClient(apirUri))
            {
                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("grant_type", "password"),
                     new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                });

                //send request
                HttpResponseMessage responseMessage = await client.PostAsync("",formContent);

                //get token from response body
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseJson);
                string token = null;
                bool isCoordinator = jObject.GetValue("isCoordinator").ToObject<bool>();
                if (isCoordinator)
                {
                    token = jObject.GetValue("access_token").ToString();
                }
                else
                {
                    throw new ArgumentNullException(null, "Login ongeldig: U heeft geen toestemming om in te loggen");
                }
                return token;

            }
        }

        public HttpClient SetUpClient(string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }
}
