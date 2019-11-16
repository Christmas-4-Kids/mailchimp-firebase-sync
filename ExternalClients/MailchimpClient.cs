using mailchimp_firebase_sync.Config;
using mailchimp_firebase_sync.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.ExternalClients
{
    public class MailchimpClient : IMailchimpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _allDayChaperonesEndpoint;
        private readonly JsonSerializerSettings _snakeCaseSerializer;

        public MailchimpClient(HttpClient httpClient, IOptions<ExternalClientsConfig> externalClientsConfig)
        {
            _httpClient = httpClient;
            var apiKey = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"conner:{externalClientsConfig.Value.MailchimpApi.ApiKey}"));
            var authHeader = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiKey);
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
            _httpClient.BaseAddress = new Uri(externalClientsConfig.Value.MailchimpApi.BaseUrl);
            _allDayChaperonesEndpoint = externalClientsConfig.Value.MailchimpApi.AllDayChaperonesEndpoint;
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            _snakeCaseSerializer = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        public async Task<MailchimpMembers> GetAllDayChaperones()
        {
            var allDayChaperonesUrl = new Uri(_allDayChaperonesEndpoint, UriKind.Relative);
            var result = await _httpClient.GetAsync(allDayChaperonesUrl);
            result.EnsureSuccessStatusCode();
            var strResult = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MailchimpMembers>(strResult, _snakeCaseSerializer);
        }
    }
}
