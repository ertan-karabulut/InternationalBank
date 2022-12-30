using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebBlazor.Models;
using WebBlazor.Services.Abstract;
using WebBlazor.Shared;

namespace WebBlazor.Services.Concreate
{
    class ServiceOperations : IServiceOperations
    {
        #region constructor
        HttpClient _httpClient;
        NavigationManager _navigation;
        ILocalStorageService _localStorage;
        private readonly IJSRuntime _jSRuntime;
        private readonly IConfiguration _configuration;
        string ApiUrl
        { 
            get 
            {
                return _configuration["ApiUrl"];
            } 
        }
        public ServiceOperations(HttpClient httpClient, NavigationManager navigation, ILocalStorageService localStorage, IJSRuntime jSRuntime, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._navigation = navigation;
            this._localStorage = localStorage;
            this._jSRuntime = jSRuntime;
            this._configuration = configuration;
        }
        #endregion

        #region local method
        async Task<T> ResultCodeControl<T>(HttpResponseMessage response)
        {
            string resultString = await response.Content.ReadAsStringAsync();
            Result result = JsonConvert.DeserializeObject<Result>(resultString);
            if (!result.ResultStatus)
            {
                switch (result.ResultCode)
                {
                    case 400:
                        var validationResult = JsonConvert.DeserializeObject<Result<List<string>>>(resultString);
                        var data = validationResult.ResultObje;
                        //txt log yapılacak.
                        throw new Exception($"Servis olumsuz cevap döndü. {JsonConvert.SerializeObject(resultString)}");
                    default:
                        break;
                }
            }
            return JsonConvert.DeserializeObject<T>(resultString);
        }
        async Task<T> Send<T>(HttpRequestMessage request)
        {
            try
            {
                await this._jSRuntime.InvokeVoidAsync("StartLoading");
                var response = await this._httpClient.SendAsync(request);
                #region status
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                        this._navigation.NavigateTo(uri: "/Login");
                        break;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.PaymentRequired:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.NotImplemented:
                    case HttpStatusCode.BadGateway:
                    case HttpStatusCode.ServiceUnavailable:
                    case HttpStatusCode.GatewayTimeout:
                    case HttpStatusCode.HttpVersionNotSupported:
                    case HttpStatusCode.VariantAlsoNegotiates:
                    case HttpStatusCode.InsufficientStorage:
                    case HttpStatusCode.LoopDetected:
                    case HttpStatusCode.NotExtended:
                    case HttpStatusCode.NetworkAuthenticationRequired:
                        this._navigation.NavigateTo(uri: "/error");
                        break;
                    default:
                        break;
                }
                #endregion
                await this._jSRuntime.InvokeVoidAsync("EndLoading");
                return await this.ResultCodeControl<T>(response);
            }
            catch (System.Exception ex)
            {
                await this._jSRuntime.InvokeVoidAsync("EndLoading");
                string message = ex.ToString();
                throw;
            }
        }
        #endregion

        public async Task<T> Get<T>(string url, object data = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.ApiUrl}{url}");
            TokenResponseModel token = await this._localStorage.GetItemAsync<TokenResponseModel>("Token");
            if (!object.Equals(token, null) && !string.IsNullOrEmpty(token.AccessToken))
                request.Headers.Add("Authorization", "Bearer " + token.AccessToken);
            if (data != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(data),
                    Encoding.UTF8, "application/json");
            return await this.Send<T>(request);
        }

        public async Task<T> Post<T>(string url, object data = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{this.ApiUrl}{url}");
            TokenResponseModel token = await this._localStorage.GetItemAsync<TokenResponseModel>("Token");
            if (!object.Equals(token ,null) && !string.IsNullOrEmpty(token.AccessToken))
                request.Headers.Add("Authorization", "Bearer " + token.AccessToken);
            if (data != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await this.Send<T>(request);
        }
    }
}
