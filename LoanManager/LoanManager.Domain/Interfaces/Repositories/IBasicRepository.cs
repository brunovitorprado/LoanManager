﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IBasicRepository<TKey, T>
    {
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAllAsync(int offset, int limit);
        Task DeleteAsync(TKey id);
        Task<int> SaveChangesAsync();
    }
}
