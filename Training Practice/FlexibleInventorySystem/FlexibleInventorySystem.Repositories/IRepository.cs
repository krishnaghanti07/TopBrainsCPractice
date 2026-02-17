using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Services;

namespace FlexibleInventorySystem.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}