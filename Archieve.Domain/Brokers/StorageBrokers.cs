using Archieve.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Domain.Brokers
{
    public partial class StorageBrokers :  IStorageBroker
    {
        private readonly ArchieveContext _Context;

        public StorageBrokers(ArchieveContext context)
        {
            _Context = context;
        }

        public async ValueTask<T> InsertAsync<T>(T @object) where T : class
        {
            await _Context.Set<T>().AddAsync(@object);
            await _Context.SaveChangesAsync();
            return @object;
        }

        public async  ValueTask<IQueryable<T>> SelectAllAsync<T>() where T : class =>
            _Context.Set<T>();
        

        public async Task<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
            await _Context.FindAsync<T>(objectIds);


    }
}

