using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class VaccineService
	{
		public static bool FillData(Vaccination vaccination)
		{
			bool status = VaccineRepository.NewEntry(vaccination);
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
			bool status = VaccineRepository.DeleteEntry(id);
			if (status)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static Vaccination? GetEntry(int id)
		{
			var entry = VaccineRepository.GetEntry(id);
			return entry;

		}
		public static List<Vaccination>? GetOrganisations()
		{
			var entry = VaccineRepository.GetVaccines();
			return entry;

		}
        
        
		public static bool ChangeEntry(Vaccination vaccination)
		{
			bool status = VaccineRepository.ChangeEntry(vaccination);
			return status;
		}


        public VaccineService()
		{
			
		}
	}
}

