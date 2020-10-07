using Blazored.LocalStorage;
using samuelenera.BlazorAdmin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Services
{
    public interface IMemberpressAPIClient
    {
        ILocalStorageService _localStorageService { get; }

        Task<MemberPressCancelSubsciptionModel> CancelSubsciptionModel(string subscriptionID, string memberID, string membershipID);
        Task<List<MemberPressBasicUserInfoModel>> GetMemberPressBasicUserInfoModelByEmail();
        Task<List<MemberPressSubscriptionInfoModel>> GetMemberPressSubcriptionsByID(string memberID);
        Task SetAuthenticatedBearerToken();
    }
}