﻿namespace Zattini.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
