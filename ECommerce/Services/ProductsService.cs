using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class ProductsService
    {
        private ProductsRepository productRepository;

        public ProductsService(ProductsRepository productsRepository)
        {
            this.productRepository = productsRepository;
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
    }
}
