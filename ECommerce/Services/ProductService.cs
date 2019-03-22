using System;
using System.Collections.Generic;
using System.Transactions;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class ProductsService
    {
        private ProductsRepository productsRepository;

        public ProductsService(ProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public List<Product> Get()
        {
            return productsRepository.Get();
        }

        public Product Get(int id)
        {
            if (id < 1)
            {
                return null;
            }
            return productsRepository.Get(id);

        }

        public bool Add (Product product)   
        {
            //Add more conditions and error messages
            if (string.IsNullOrEmpty(product.Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(product.Description))
            {
                return false;
            }
            if (product.Price < 0)
            {
                return false;
            }
            this.productsRepository.Add(product);
            return true;
        }

        public bool Delete (int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var deleteItem = this.productsRepository.Get(id);
                if (deleteItem == null)
                {
                    return false;
                }
                this.productsRepository.Delete(id);
                scope.Complete();
            }
            return true;    
        }
    }
}
