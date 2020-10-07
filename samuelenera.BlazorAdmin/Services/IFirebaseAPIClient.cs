using Blazored.LocalStorage;
using samuelenera.BlazorAdmin.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Services
{
    public interface IFirebaseAPIClient
    {
        ILocalStorageService _localStorageService { get; }

        Task<FirebaseSubscriberDevicesModel> GetSubscriberDevices(string SubscriberID);
        Task<HttpStatusCode> InserSubscribertDevice(string SubscriberID, List<FirebaseSubscriberDevicesModel.Device> firebaseSubscriberDevice);
        Task<List<FirebaseSubscriberDevicesModel>> UpdateInsertRecord(string SubscriberID, FirebaseSubscriberDevicesModel firebaseSubscriberDevicesModel);
    }
}