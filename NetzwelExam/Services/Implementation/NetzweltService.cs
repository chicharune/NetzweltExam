using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NetzweltExam.Controllers;
using NetzweltExam.Models;
using Newtonsoft.Json;

namespace NetzweltExam.Services
{
    public class NetzweltService : INetzweltService
    {
        public async Task<UserModel> GetUser(LoginModel model)
        {
            UserModel user = null;

            var client = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://netzwelt-devtest.azurewebsites.net/Account/SignIn");

            var jsonString = JsonConvert.SerializeObject(model);

            var requestContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            httpRequestMessage.Content = requestContent;

            var response = client.SendAsync(httpRequestMessage).Result;

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    user = JsonConvert.DeserializeObject<UserModel>(content.ReadAsStringAsync().Result);
                }
            }

            return user;
        }

        public async Task<DataModel> GetTerritory()
        {
            DataModel territories = new DataModel();

            var client = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://netzwelt-devtest.azurewebsites.net/Territories/All");

            var response = client.SendAsync(httpRequestMessage).Result;

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    territories = JsonConvert.DeserializeObject<DataModel>(content.ReadAsStringAsync().Result);
                }
            }

            return territories;
        }
    }
}
