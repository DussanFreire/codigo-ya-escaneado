using BackEndAlbergue.Data.Entities;
using BackEndAlbergue.Models.Auths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Data.Repository
{
    public interface IRefuge
    {
        //PETS
        public IEnumerable<PetEntity> GetPets();
        public PetEntity GetPet(int petId);
        public PetEntity CreatePet(PetEntity petEntity);
        public bool DeletePet(int petId);
        public PetEntity UpdatePet(PetEntity petEntity);
        //NOTICES
        public IEnumerable<NoticeEntity> GetNotices();
        public NoticeEntity GetNotice(int NoticeId);
        public NoticeEntity CreateNotice(NoticeEntity noticeEntity);
        public bool DeleteNotice(int NoticeId);
        public NoticeEntity UpdateNotice(NoticeEntity noticeEntity);
        //PETSHOP
        public IEnumerable<ProductEntity> GetProducts();
        public ProductEntity GetProduct(int ItemId);
        public ProductEntity CreateProduct(ProductEntity productEntity);
        public bool DeleteProduct(int ItemId);
        public ProductEntity UpdateProduct(ProductEntity productEntity);

        //Auth
        public IEnumerable<UserModel> GetUsers();
    }
}
