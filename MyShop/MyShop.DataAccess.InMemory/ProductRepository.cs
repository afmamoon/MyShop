using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> List_Of_Products;

        public ProductRepository()
        {
            List_Of_Products = cache["products"] as List<Product>;  // Load the Cache called "products" into List products
            
            if (List_Of_Products == null)   // If List Called list_of_products is not Found then create below.
            {
                List_Of_Products = new List<Product>();
            }

        }

        public void Commit()
        {
            cache["products"] = List_Of_Products;
        }

        public void Insert(Product p)
        {
            List_Of_Products.Add(p);
        }

        public void Update(Product product)
        {
            Product producttoupdate = List_Of_Products.Find(p => p.Id == product.Id);

            if (producttoupdate != null)
            {
                producttoupdate = product; 
            }

            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public Product Find(string Id)
        {
            Product product = List_Of_Products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }

            else
            {
                throw new Exception("Product Not Found");
            }

        }

        public IQueryable<Product> Collection () 
        {
            return List_Of_Products.AsQueryable();
        }

        public void Delete (string Id)
        {
            Product ProductToDelete = List_Of_Products.Find(p => p.Id == Id);

            if (ProductToDelete != null)
            {
                List_Of_Products.Remove(ProductToDelete);

            }

            else
            {
                throw new Exception("Nothing to Delete, Product was Not Found");
            }


        }


    }
}
