using System;
using System.Collections.Generic;
using System.Transactions;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class ProductService
    {
        private ProductRepository productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> Get()
        {
            return productRepository.Get();
        }

        public Product Get(int id)
        {
            if (id < 1)
            {
                return null;
            }
            return productRepository.Get(id);

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
            this.productRepository.Add(product);
            return true;
        }

        public bool Delete (int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var deleteItem = this.productRepository.Get(id);
                if (deleteItem == null)
                {
                    return false;
                }
                this.productRepository.Delete(id);
                scope.Complete();
            }
            return true;    
        }
    }
}
