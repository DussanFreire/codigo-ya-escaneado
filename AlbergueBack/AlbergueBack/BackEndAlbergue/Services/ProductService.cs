using AutoMapper;
using BackEndAlbergue.Data.Entities;
using BackEndAlbergue.Data.Repository;
using BackEndAlbergue.Exceptions;
using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public class ProductService : IProductService
    {
        private readonly IRefuge _refugeRepository;
        private readonly IMapper _mapper;

        public ProductService(IRefuge refugeRepository, IMapper _mapper)
        {
            this._refugeRepository = refugeRepository;
            this._mapper = _mapper;
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            var entityList = _refugeRepository.GetProducts();
            var modelList = _mapper.Map<IEnumerable<ProductModel>>(entityList);
            return modelList;
        }

        public ProductModel GetProduct(int itemPetShopId)
        {
            var petShopItem= _refugeRepository.GetProduct(itemPetShopId);

            if (petShopItem == null)
            {
                throw new NotFoundException($"The item {itemPetShopId} doesnt exists in the petshop, try it with a new id");
            }

            return _mapper.Map<ProductModel>(petShopItem);
        }
        public ProductModel CreateProduct(ProductModel petShopModel)
        {
            var entityreturned = _refugeRepository.CreateProduct(_mapper.Map<ProductEntity>(petShopModel));

            return _mapper.Map<ProductModel>(entityreturned);
        }

        public bool DeleteProduct(int itemPetShopId)
        {
            GetProduct(itemPetShopId);
            return _refugeRepository.DeleteProduct(itemPetShopId);
        }
        public ProductModel UpdateProduct(int PetShopItemId, ProductModel petShopModel)
        {
            petShopModel.Id = PetShopItemId;
            var petShopItemToUpdate = _refugeRepository.UpdateProduct(_mapper.Map<ProductEntity>(petShopModel));
            return _mapper.Map<ProductModel>(petShopItemToUpdate);
        }
    }
}
