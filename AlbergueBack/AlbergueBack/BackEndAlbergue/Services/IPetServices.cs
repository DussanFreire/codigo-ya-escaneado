using BackEndAlbergue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Services
{
    public interface IPetServices
    {
        public IEnumerable<PetModel> GetPets();
        public PetModel GetPet(int petId);
        public PetModelImpostor CreatePet(PetModel petModel);
        public bool DeletePet(int petId);
        public PetModelImpostor UpdatePet(int petId, PetModel petModel);
    }
}
