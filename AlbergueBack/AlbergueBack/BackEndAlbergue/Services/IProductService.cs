using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public interface IProductService
    {
        public IEnumerable<ProductModel> GetProducts();
        public ProductModel GetProduct(int itemPetShopId);
        public ProductModel CreateProduct(ProductModel petShopModel);
        public bool DeleteProduct(int itemPetShopId);
        public ProductModel UpdateProduct(int PetShopItemId, ProductModel petShopModel);
    }
}
