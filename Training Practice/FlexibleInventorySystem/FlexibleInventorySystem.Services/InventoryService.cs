using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Repositories;

namespace FlexibleInventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<Type, object> _repositories = new();

        private IRepository<T> GetRepository<T>() where T : Product
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new InMemoryRepository<T>();
            }

            return (IRepository<T>)_repositories[type];
        }

        public void AddProduct<T>(T product) where T : Product
        {
            GetRepository<T>().Add(product);
        }

        public void UpdateProduct<T>(T product) where T : Product
        {
            GetRepository<T>().Update(product);
        }

        public void RemoveProduct<T>(Guid id) where T : Product
        {
            GetRepository<T>().Remove(id);
        }

        public T GetProductById<T>(Guid id) where T : Product
        {
            return GetRepository<T>().GetById(id);
        }

        public IEnumerable<T> GetProductsByCategory<T>() where T : Product
        {
            return GetRepository<T>().GetAll();
        }
    }
}
