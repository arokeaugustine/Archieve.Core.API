using Archieve.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Domain.Brokers
{
    public partial interface IStorageBroker
    {
        Task<Book> InsertBookAsync(Book books);
        Task<IQueryable<Book>> SelectAllBooksAsync();
        Task<Book> SelectBookByIdAsync(int id);
    }
}
