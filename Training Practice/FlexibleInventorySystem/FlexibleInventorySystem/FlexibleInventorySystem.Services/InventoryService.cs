using System;
using System.Collections.Generic;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Repositories;

namespace FlexibleInventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        // TODO: Maintain repositories per type

        public void AddProduct<T>(T product) where T : Product
        {
            // TODO: Add product logic
        }

        public void UpdateProduct<T>(T product) where T : Product
        {
            // TODO: Update product logic
        }

        public void RemoveProduct<T>(Guid id) where T : Product
        {
            // TODO: Remove product logic
        }

        public T GetProductById<T>(Guid id) where T : Product
        {
            // TODO: Get product by Id
            return null;
        }

        public IEnumerable<T> GetProductsByCategory<T>() where T : Product
        {
            // TODO: Return products of category
            return new List<T>();
        }
    }
}