using BookStore_API.Contracts;
using BookStore_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Services
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<bool> Create(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Book>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<Book> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
