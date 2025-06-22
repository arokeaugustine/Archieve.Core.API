using Archieve.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Domain.Brokers
{
    public partial class StorageBrokers
    {
        public async Task<Book> InsertBookAsync(Book books) =>
            await InsertAsync(books);

        public async Task<IQueryable<Book>> SelectAllBooksAsync() => 
            await SelectAllAsync<Book>();

        public async Task<Book> SelectBookByIdAsync(int id) => 
            await SelectAsync<Book>(id);

    }
}
