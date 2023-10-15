using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class OrganisationService
	{
		private static List<Organisation> Organisations { get; set; } = new List<Organisation>();

		public static bool FillData(Organisation organisation)
		{
			bool status = OrganisationsRepository.AddOrganisation(organisation);
			return status;
		}

		public static bool DeleteEntry(int id)
		{
			bool status = OrganisationsRepository.DeleteOrganisation(OrganisationsRepository.GetOrganisationById(id));
			return status;
		}

		public static Organisation? GetEntry(int id)
		{
			var entry = OrganisationsRepository.GetOrganisationById(id);
			return entry;
		}

		public static List<Organisation>? GetOrganisations(string filterField, string? filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
		{
			filterValue = filterValue?.ToLower();

			// Применение фильтрации в зависимости от поля
			if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
			{
				switch (filterField.ToLower())
				{
					case "orgname":
						Organisations = OrganisationsRepository.GetOrganisationsByName(filterValue).ToList();
						break;
					case "inn":
						Organisations = OrganisationsRepository.GetOrganisationsByINN(filterValue).ToList();
						break;
					case "kpp":
                        Organisations = OrganisationsRepository.GetOrganisationsByKPP(filterValue).ToList();
                        break;
					// Добавьте остальные варианты полей
					default:
						break;
				}
			}
        
			// Сортировка
			if (!string.IsNullOrEmpty(sortBy))
			{
				switch (sortBy)
				{
					case "OrgId":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.OrgId).ToList() : Organisations.OrderByDescending(a => a.OrgId).ToList();
						break;
					case "OrgName":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.OrgName).ToList() : Organisations.OrderByDescending(a => a.OrgName).ToList();
						break;
					case "INN":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.INN).ToList() : Organisations.OrderByDescending(a => a.INN).ToList();
						break;
					case "KPP":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.KPP).ToList() : Organisations.OrderByDescending(a => a.KPP).ToList();
						break;
					case "AdressReg":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.AdressReg).ToList() : Organisations.OrderByDescending(a => a.AdressReg).ToList();
						break;
					case "TypeOrg":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.OrgType.NameOrgType).ToList() : Organisations.OrderByDescending(a => a.OrgType.NameOrgType).ToList();
						break;
					case "Locality":
                        Organisations = isAscending ? Organisations.OrderBy(a => a.Locality.NameLocality).ToList() : Organisations.OrderByDescending(a => a.Locality.NameLocality).ToList();
						break;
				}
			}
        
			// Пагинация
			var organisationsPag = Organisations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        
			return organisationsPag;
		}
        
        
		public static bool ChangeEntry(Organisation organisation)
		{
			bool status = OrganisationsRepository.UpdateOrganisaton(organisation);
			return status;
		}
		
		public static int GetTotalOrganisations(string filterField, string? filterValue)
		{
			return Organisations.Count();
		}


  //      public OrganizationService()
		//{
			
		//}
	}
}

