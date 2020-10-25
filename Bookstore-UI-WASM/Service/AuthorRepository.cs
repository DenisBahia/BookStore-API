using Blazored.LocalStorage;
using Bookstore_UI_WASM.Contracts;
using Bookstore_UI_WASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bookstore_UI_WASM.Service
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        public AuthorRepository(HttpClient client,
            ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
    }
}
