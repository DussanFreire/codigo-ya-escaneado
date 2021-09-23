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
        public PetModel GetPet(int PetId);
        public PetModelImpostor CreatePet(PetModel petModel);
        public bool DeletePet(int PetId);
        public PetModelImpostor UpdatePet(int PetId, PetModel petModel);
    }
}
