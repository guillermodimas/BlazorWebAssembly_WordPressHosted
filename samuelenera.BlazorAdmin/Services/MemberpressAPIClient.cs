using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using samuelenera.BlazorAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Services
{
    public class MemberpressAPIClient : IMemberpressAPIClient
    {
        private HttpClient _apiClient;
        private readonly IConfiguration _config;
        public ILocalStorageService _localStorageService { get; }
        public MemberpressAPIClient(IConfiguration config, ILocalStorageService localStorageService) //constructor
        {
            _config = config;
            _localStorageService = localStorageService;
            InitializeClient();

        }
        private void InitializeClient()
        {
            string api = _config["WPDomain"]; //ConfigurationManager.AppSettings["api"]; //api URL from app.config
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task SetAuthenticatedBearerToken()
        {




            var token = await _localStorageService.GetItemAsync<string>("memberpressAccessToken");

            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            await Task.FromResult("");
        }

        public async Task<List<MemberPressBasicUserInfoModel>> GetMemberPressBasicUserInfoModelByEmail()
        {
            await SetAuthenticatedBearerToken();
            var userEmail = await _localStorageService.GetItemAsync<string>("user_email");

            List<MemberPressBasicUserInfoModel> user = new List<MemberPressBasicUserInfoModel>();

            using (HttpResponseMessage response = await _apiClient.GetAsync("wp-json/mp/v1/members?search=" + userEmail))
            {

                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadFromJsonAsync<List<MemberPressBasicUserInfoModel>>();
                    return user;
                }
                else
                {

                    return user;
                }
            }
        }
        public async Task<List<MemberPressSubscriptionInfoModel>> GetMemberPressSubcriptionsByID(string memberID)
        {
            await SetAuthenticatedBearerToken();


            List<MemberPressSubscriptionInfoModel> user = new List<MemberPressSubscriptionInfoModel>();

            using (HttpResponseMessage response = await _apiClient.GetAsync("wp-json/mp/v1/subscriptions?member=" + memberID))
            {

                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadFromJsonAsync<List<MemberPressSubscriptionInfoModel>>();
                    return user;
                }
                else
                {

                    return user;
                }
            }
        }
        public async Task<MemberPressCancelSubsciptionModel> CancelSubsciptionModel(string subscriptionID, string memberID, string membershipID)
        {
            await SetAuthenticatedBearerToken();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("member", memberID),
                new KeyValuePair<string, string>("membership", membershipID),
                new KeyValuePair<string, string>("status", "cancelled"),
                new KeyValuePair<string, string>("created_at", DateTime.UtcNow.ToString())
            });

            MemberPressCancelSubsciptionModel cancelResult = new MemberPressCancelSubsciptionModel();

            using (HttpResponseMessage response = await _apiClient.PostAsync("wp-json/mp/v1/subscriptions/" + subscriptionID, data))
            {

                if (response.IsSuccessStatusCode)
                {
                    cancelResult = await response.Content.ReadFromJsonAsync<MemberPressCancelSubsciptionModel>();
                    return cancelResult;
                }
                else
                {

                    return cancelResult;
                }
            }
        }
    }
}
