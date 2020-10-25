using Blazored.LocalStorage;
using Bookstore_UI_WASM.Contracts;
using Bookstore_UI_WASM.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_UI_WASM.Service
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseRepository(HttpClient client,
            ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<bool> Create(string url, T obj)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await _client.PostAsJsonAsync<T>(url, obj);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;

            /*
            if (obj == null)
            {
                return false;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            
            var client = _client.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
            */

        }

        public async Task<bool> Delete(string url, int id)
        {

            if (id <= 0)
            {
                return false;
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await _client.DeleteAsync(url + id);

            /*
            var request = new HttpRequestMessage(HttpMethod.Delete, url + id);
            var client = _client.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            */

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;

        }

        public async Task<T> Get(string url, int id)
        {

            if (id <= 0)
            {
                return null;
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var response = await _client.GetFromJsonAsync<T>(url+id);

            /*

            var request = new HttpRequestMessage(HttpMethod.Get, url + id);

            var client = _client.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            */

            return response;

        }

        public async Task<IList<T>> Get(string url)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var response = await _client.GetFromJsonAsync<IList<T>>(url);

            return response;

        }

        public async Task<bool> Update(string url, T obj, int id)
        {

            if (obj == null)
            {
                return false;
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var response = await _client.PutAsJsonAsync<T>(url+id, obj);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;

        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
