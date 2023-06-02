using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class AnimalService
	{
        public static bool FillData(Animal animal)
		{
			bool status = AnimalRepository.NewEntry(animal);
			if (status)
			{
				return true;
			}
			else
			{
				return false;
			}
        }

        public static bool DeleteEntry(int id)
        {
            bool status = AnimalRepository.DeleteEntry(id);
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Animal? GetEntry(int id)
        {
            var entry = AnimalRepository.GetEntry(id);
            return entry;

        }
        public static List<Animal>? GetAnimals()
        {
	        var entry = AnimalRepository.GetAnimals();
	        return entry;

        }
        
        
        public static bool ChangeEntry(Animal animal)
        {
	        bool status = AnimalRepository.ChangeEntry(animal);
	        return status;
        }

        public AnimalService()
		{
			
		}
	}
}

