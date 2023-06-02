using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class OrganizationService
	{
		public static bool FillData(Organisation organisation)
		{
			bool status = OrganisationsRepository.NewEntry(organisation);
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
			bool status = OrganisationsRepository.DeleteEntry(id);
			if (status)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static Organisation? GetEntry(int id)
		{
			var entry = OrganisationsRepository.GetEntry(id);
			return entry;
		}

		public static List<Organisation>? GetOrganisations()
		{
			var entry = OrganisationsRepository.GetOrganizations();
			return entry;
		}
        
        
		public static bool ChangeEntry(Organisation organisation)
		{
			bool status = OrganisationsRepository.ChangeEntry(organisation);
			return status;
		}


        public OrganizationService()
		{
			
		}
	}
}

