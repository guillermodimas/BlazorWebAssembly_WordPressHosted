using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using samuelenera.BlazorAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Services
{
    public class FirebaseAPIClient : IFirebaseAPIClient
    {
        private HttpClient _apiClient;
        private readonly IConfiguration _config;
        public ILocalStorageService _localStorageService { get; }

        private string APIKEY;
        public FirebaseAPIClient(IConfiguration config, ILocalStorageService localStorageService) //constructor
        {
            _config = config;
            _localStorageService = localStorageService;
            InitializeClient();

        }
        private void InitializeClient()
        {
            string api = _config["FirebaseDatabaseEndpoint"]; //ConfigurationManager.AppSettings["api"]; //api URL from app.config
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            APIKEY = "qfANm93JdR7FkEshFtBwQgmdwmwTG3RpCEmMHtXI";
        }


        public async Task<FirebaseSubscriberDevicesModel> GetSubscriberDevices(string SubscriberID)
        {


            FirebaseSubscriberDevicesModel list = new FirebaseSubscriberDevicesModel();

            using (HttpResponseMessage response = await _apiClient.GetAsync($"Subscribers/{SubscriberID}.json?auth=" + APIKEY))
            {

                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadFromJsonAsync<FirebaseSubscriberDevicesModel>();
                    return list;
                }
                else
                {

                    return list;
                }
            }
        }
        public async Task<List<FirebaseSubscriberDevicesModel>> UpdateInsertRecord(string SubscriberID, FirebaseSubscriberDevicesModel firebaseSubscriberDevicesModel)
        {

            List<FirebaseSubscriberDevicesModel> list = new List<FirebaseSubscriberDevicesModel>();

            using (HttpResponseMessage response = await _apiClient.PutAsJsonAsync<FirebaseSubscriberDevicesModel>($"Subscribers/{SubscriberID}.json?auth=" + APIKEY, firebaseSubscriberDevicesModel))
            {

                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadFromJsonAsync<List<FirebaseSubscriberDevicesModel>>();
                    return list;
                }
                else
                {

                    return list;
                }
            }
        }
        public async Task<HttpStatusCode> InserSubscribertDevice(string SubscriberID, List<FirebaseSubscriberDevicesModel.Device> firebaseSubscriberDevice)
        {

            List<FirebaseSubscriberDevicesModel.Device> list = new List<FirebaseSubscriberDevicesModel.Device>();

            using (HttpResponseMessage response = await _apiClient.PutAsJsonAsync<List<FirebaseSubscriberDevicesModel.Device>>($"Subscribers/{SubscriberID}/devices/.json?auth=" + APIKEY, firebaseSubscriberDevice))
            {

                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
                else
                {
                    return response.StatusCode;
                }
            }
        }

    }
}
