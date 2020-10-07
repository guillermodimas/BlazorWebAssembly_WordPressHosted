using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using samuelenera.BlazorAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public HttpClient _httpClient { get;  }


        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           

            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
            var expirationDate = await _localStorageService.GetItemAsync<string>("exp_dateTime");
            var userEmail = await _localStorageService.GetItemAsync<string>("user_email");
            var userNiceName = await _localStorageService.GetItemAsync<string>("user_nicename");
            var userDisplayName = await _localStorageService.GetItemAsync<string>("user_display_name");
            var memberpressAccessToken = await _localStorageService.GetItemAsync<string>("memberpressAccessToken");

            ClaimsIdentity identity;
            


            if (accessToken != null && accessToken != string.Empty)
            {
                if (expirationDate != null && Convert.ToDateTime(expirationDate) > DateTime.Now)
                {
                    AuthenticatedUserModel authenticatedUserModel = new AuthenticatedUserModel {
                        token = accessToken,
                        user_email = userEmail,
                        user_nicename = userNiceName,
                        user_display_name = userDisplayName
                    };
                    
                   
                    identity = GetClaimsIdentity(authenticatedUserModel); //get claims 
                }
                else //if access token expired, clear identity
                {

                    //clear identity
                    await _localStorageService.RemoveItemAsync("accessToken");
                    await _localStorageService.RemoveItemAsync("exp_dateTime");
                    await _localStorageService.RemoveItemAsync("user_email");
                    await _localStorageService.RemoveItemAsync("user_nicename");
                    await _localStorageService.RemoveItemAsync("user_display_name");
                    await _localStorageService.RemoveItemAsync("memberpressAccessToken"); 

                    identity = new ClaimsIdentity();
                    
                    
                }



            }
            else
            {
                identity = new ClaimsIdentity(); //clear identity
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task<AuthenticatedUserModel> RetrieveMemberPressToken()
        {

            WPUserLoginModel wPUserLoginModel = new WPUserLoginModel { username = "dimas.guillermo@rocketmail.com", password = "AL^R2!Dl&CAbHA8%*jHb#uf%" };
            AuthenticatedUserModel authenticatedUserModel = new AuthenticatedUserModel();
            using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync("wp-json/jwt-auth/v1/token", wPUserLoginModel))
            {
                if (response.IsSuccessStatusCode)
                {
                   

                    authenticatedUserModel = await response.Content.ReadFromJsonAsync<AuthenticatedUserModel>();

                    
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    string test = "ERROR: Your username or password was incorrect";
                }

            }

            return await Task.FromResult(authenticatedUserModel);
        }
        public async Task MarkUserAsAuthenticated(AuthenticatedUserModel user)
        {

            var memberPressAuthUser = await RetrieveMemberPressToken();

            await _localStorageService.SetItemAsync<string>("accessToken", user.token);
            await _localStorageService.SetItemAsync<string>("exp_dateTime", DateTime.UtcNow.ToLocalTime().AddHours(1).ToString());
            await _localStorageService.SetItemAsync<string>("user_email", user.user_email);
            await _localStorageService.SetItemAsync<string>("user_nicename", user.user_nicename);
            await _localStorageService.SetItemAsync<string>("user_display_name", user.user_display_name);

            await _localStorageService.SetItemAsync<string>("memberpressAccessToken", memberPressAuthUser.token);


            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("accessToken");
            await _localStorageService.RemoveItemAsync("exp_dateTime");
            await _localStorageService.RemoveItemAsync("user_email");
            await _localStorageService.RemoveItemAsync("user_nicename");
            await _localStorageService.RemoveItemAsync("user_display_name");
            await _localStorageService.RemoveItemAsync("memberpressAccessToken");


            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(AuthenticatedUserModel user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.user_email != null)
            {
                Claim[] claimarray;

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.user_display_name));
                claims.Add(new Claim(ClaimTypes.Email, user.user_email));
                claimarray = claims.ToArray();
                claimsIdentity = new ClaimsIdentity(claimarray, "apiauth_type");
            }

            return claimsIdentity;
        }

    }
}
