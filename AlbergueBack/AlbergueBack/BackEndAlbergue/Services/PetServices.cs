using BackEndAlbergue.Data.Entities;
using BackEndAlbergue.Exceptions;
using AutoMapper;
using BackEndAlbergue.Data.Repository;
using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public class PetServices : IPetServices
    {
        private IRefuge _refugeRepository;
        private IMapper _mapper;

        public PetServices(IRefuge refugeRepository, IMapper _mapper)
        {
            this._refugeRepository = refugeRepository;
            this._mapper = _mapper;
        }

        public IEnumerable<PetModel> GetPets()
        {
            var entityList = _refugeRepository.GetPets();
            var modelListImpostor = _mapper.Map<IEnumerable<PetModelImpostor>>(entityList);
            List<PetModel> pets = new List<PetModel>();

            foreach (var petimpostor in modelListImpostor)
            {
                PetModel pet = new PetModel();
                pet.Id = petimpostor.Id;
                pet.Name = petimpostor.Name;
                pet.Sex = petimpostor.Sex;
                pet.Description = petimpostor.Description;
                pet.Photo = petimpostor.Photo;
                pet.Vaccines = petimpostor.Vaccines;
                pet.Sterilization = petimpostor.Sterilization;
                pet.Size = petimpostor.Size;
                pet.previous = petimpostor.previous;
                pet.next = petimpostor.next;
                pet.IsAdopted = petimpostor.IsAdopted;

                DateTime DateNow = DateTime.Now;
                DateTime? dateOri = petimpostor.age;

                var difference = DateNow - dateOri;
                var months = (int)((difference).Value.TotalDays / 30.25);
                pet.age = months;
                pets.Add(pet);

            }
            
            return pets;
        }
        public PetModel GetPet(int petId)
        {
            var petimpostor = _refugeRepository.GetPet(petId);
            var pet = new PetModel();

            if (petimpostor == null)
            {
                throw new NotFoundException($"The pet {petId} doesnt exists, try it with a new id");
            }
            pet.Id = petimpostor.Id;
            pet.Name = petimpostor.Name;
            pet.Sex = petimpostor.Sex;
            pet.Description = petimpostor.Description;
            pet.Photo = petimpostor.Photo;
            pet.Vaccines = petimpostor.Vaccines;
            pet.Sterilization = petimpostor.Sterilization;
            pet.Size = petimpostor.Size;
            pet.previous = petimpostor.previous;
            pet.next = petimpostor.next;
            pet.IsAdopted=petimpostor.IsAdopted;

            DateTime DateNow = DateTime.Now;
            DateTime? dateOri = petimpostor.age;

            var difference = DateNow - dateOri;
            var months = (int)((difference).Value.TotalDays / 30.25);
            pet.age = months;

            return _mapper.Map<PetModel>(pet);

        }

        public PetModelImpostor CreatePet(PetModel petModel)
        {
            var impostor = new PetModelImpostor();

            impostor.Name = petModel.Name;
            impostor.Sex = petModel.Sex;
            impostor.Description = petModel.Description;
            impostor.IsAdopted = petModel.IsAdopted;
            impostor.Photo = petModel.Photo;
            impostor.Vaccines = petModel.Vaccines;
            impostor.Sterilization = petModel.Sterilization;
            impostor.Size = petModel.Size;
            impostor.previous = petModel.previous;
            impostor.next = petModel.next;

            var DateNow = DateTime.Now;
            var auxAge = (int)petModel.age*30.25*-1;
            var DateModified = DateNow.AddDays( auxAge);
            impostor.age = DateModified;


            var entityreturned = _refugeRepository.CreatePet(_mapper.Map<PetEntity>(impostor));

            return _mapper.Map<PetModelImpostor>(entityreturned);
        }

        public bool DeletePet(int petId)
        {
            var breedToDelete = GetPet(petId);
            return _refugeRepository.DeletePet(petId);
        }

        public PetModelImpostor UpdatePet(int petId, PetModel petModel)
        {
            
            var impostor = new PetModelImpostor();
            impostor.Id = petId;
            impostor.Name = petModel.Name;
            impostor.Sex = petModel.Sex;
            impostor.Description = petModel.Description;
            impostor.IsAdopted = petModel.IsAdopted;
            impostor.Photo = petModel.Photo;
            impostor.Vaccines = petModel.Vaccines;
            impostor.Sterilization = petModel.Sterilization;
            impostor.Size = petModel.Size;
            impostor.previous = petModel.previous;
            impostor.next = petModel.next;
            if (impostor.age != null)
            {
                var DateNow = DateTime.Now;

                var auxAge = (int)petModel.age * 30.25 * -1;
                var DateModified = DateNow.AddDays(auxAge);
                impostor.age = DateModified;
            }
            var petToUpdate = _refugeRepository.UpdatePet(_mapper.Map<PetEntity>(impostor));
            return _mapper.Map<PetModelImpostor>(petToUpdate);
        }
    }
}
