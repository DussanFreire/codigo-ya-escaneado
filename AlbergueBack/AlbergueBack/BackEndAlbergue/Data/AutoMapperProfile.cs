using BackEndAlbergue.Data.Entities;
using AutoMapper;
using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<PetEntity, PetModel>()
            .ReverseMap();

            this.CreateMap<PetModel, PetEntity>()
                   .ReverseMap();
            this.CreateMap<NoticeEntity, NoticeModel>()
                    .ReverseMap();

            this.CreateMap<NoticeModel, NoticeEntity>()
                   .ReverseMap();
            this.CreateMap<ProductEntity, ProductModel>()
                    .ReverseMap();

            this.CreateMap<ProductModel, ProductEntity>()
                   .ReverseMap();
            this.CreateMap<PetModelImpostor, PetEntity>()
                   .ReverseMap();
            this.CreateMap<PetEntity, PetModelImpostor>()
                  .ReverseMap();
        }
    }
}
