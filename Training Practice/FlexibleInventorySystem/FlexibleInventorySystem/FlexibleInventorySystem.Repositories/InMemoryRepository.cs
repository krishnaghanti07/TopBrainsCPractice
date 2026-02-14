using System;
using System.Collections.Generic;

namespace FlexibleInventorySystem.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _storage = new List<T>();

        public void Add(T entity)
        {
            // TODO: Add entity to storage
        }

        public void Update(T entity)
        {
            // TODO: Update entity
        }

        public void Remove(Guid id)
        {
            // TODO: Remove entity by Id
        }

        public T GetById(Guid id)
        {
            // TODO: Retrieve entity by Id
            return default;
        }

        public IEnumerable<T> GetAll()
        {
            // TODO: Return all entities
            return new List<T>();
        }
    }
}