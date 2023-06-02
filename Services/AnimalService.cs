using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class AnimalService
	{
        public static bool FillData(Animal animal)
		{
			bool status = AnimalRepositorys.NewEntry(animal);
			if (status)
			{
				return true;
			}
			else
			{
				return false;
			}
        }

        public AnimalService()
		{
			
		}
	}
}

