using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using IpCorpTestApi.Responses;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Globalization;


namespace IpCorpTestApi
{
    public class IpCorpApiClient
    {
        private readonly string _service ;
        private readonly string _tokenService ;
        private readonly string _grantType ;    
        private readonly string _username;
        private readonly string _password;    
        private readonly string _clientID;             

        public IpCorpApiClient(
            string service,
            string tokenService,
            string grantType,
            string username,
            string password,
            string clientID)  
        {  
            _service = service;
            _tokenService = tokenService;
            _grantType = grantType;
            _username = username;
            _password = password;
            _clientID = clientID;                                                        
        }  

        public async Task<List<LogSistemaResponse>> GetLogs(string queryString = "")  
        {  
            Uri requestUrl = CreateRequestUri(string.Format(CultureInfo.InvariantCulture, "LogSistema"),queryString);
            return await GetAsync<List<LogSistemaResponse>>(requestUrl);  
        }


        //Monta a url completa: url principal+ path do serviço + queryString
        private Uri CreateRequestUri(string relativePath, string queryString = "")  
        {  
            var endpoint = new Uri(new Uri(_service), relativePath);  
            var uriBuilder = new UriBuilder(endpoint);  
            uriBuilder.Query = queryString;  
            return uriBuilder.Uri;  
        }  

        private async Task<T> GetAsync<T>(Uri requestUrl)  
        {  
            Task<string> responseToken = GetToken();
            
            HttpClient httpClient = new HttpClient();   
            httpClient.DefaultRequestHeaders.Add("Authorization","Bearer " + responseToken.Result);            
            var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);  
            response.EnsureSuccessStatusCode();  
            string data = await response.Content.ReadAsStringAsync();  
            JObject retorno = JObject.Parse(data);
            return JsonConvert.DeserializeObject<T>(retorno["Results"].ToString());  
        }

        private async Task<string> GetToken ()  
        {  
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_tokenService);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Service/oauth2/Token");
            
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", _grantType));
            keyValues.Add(new KeyValuePair<string, string>("username", _username));
            keyValues.Add(new KeyValuePair<string, string>("password", _password));
            keyValues.Add(new KeyValuePair<string, string>("client_id", _clientID));           
            
            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();  
            string data = await response.Content.ReadAsStringAsync();
            JObject retorno = JObject.Parse(data);             
            return retorno["access_token"].ToString();
        }         


    }
}
