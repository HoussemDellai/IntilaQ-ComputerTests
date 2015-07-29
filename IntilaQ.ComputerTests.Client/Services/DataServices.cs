using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IntilaQ.ComputerTests.Client.Models;
using Newtonsoft.Json;

namespace IntilaQ.ComputerTests.Client.Services
{
    public class DataServices
    {

        private const string BaseUrl = "https://intilaq-computertests.azurewebsites.net/";
        //private const string BaseUrl = "https://localhost:44300/";
        public async Task<List<AnswerTest>> GetAnswerTestsAsync()
        {
            var httpClient = new HttpClient();

            var eventItemsJson =
                await httpClient.GetStringAsync(BaseUrl + "api/Tests");

            return JsonConvert.DeserializeObject<List<AnswerTest>>(eventItemsJson);
        }

        public async Task<int> SendCandidateUserAsync(CandidateUser candidateUser)
        {
            
            var jsonCandidateTestRequest = JsonConvert.SerializeObject(candidateUser);

            HttpContent httpContent = new StringContent(jsonCandidateTestRequest);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.PostAsync(new Uri(BaseUrl + "api/Tests"), httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var scoreJson = await response.Content.ReadAsStringAsync();

                        var score = JsonConvert.DeserializeObject<int>(scoreJson);

                        return score;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
