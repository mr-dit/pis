using System;
using pis_web_api.Models;
using pis_web_api.Repositorys;

namespace pis_web_api.Services
{
	public class OrganisationService
	{
		public static bool FillData(Organisation organisation)
		{
			bool status = OrganisationsRepository.NewEntry(organisation);
			return status;
		}

		public static bool DeleteEntry(int id)
		{
			bool status = OrganisationsRepository.DeleteEntry(id);
			return status;
		}

		public static Organisation? GetEntry(int id)
		{
			var entry = OrganisationsRepository.GetEntry(id);
			return entry;
		}

		public static List<Organisation>? GetOrganisations(string? filterField, string? filterValue, string? sortBy, bool isAscending, int pageNumber, int pageSize)
		{
			filterValue = filterValue?.ToLower();
			
			var organisations = OrganisationsRepository.GetOrganizations();

			// Применение фильтрации в зависимости от поля
			if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
			{
				switch (filterField.ToLower())
				{
					case "orgname":
						organisations = organisations.Where(o => o.OrgName.ToLower().Contains(filterValue)).ToList();
						break;
					case "inn":
						organisations = organisations.Where(o => o.INN.ToString().Contains(filterValue)).ToList();
						break;
					case "kpp":
						organisations = organisations.Where(o => o.KPP.ToString().Contains(filterValue)).ToList();
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
						organisations = isAscending ? organisations.OrderBy(a => a.OrgId).ToList() : organisations.OrderByDescending(a => a.OrgId).ToList();
						break;
					case "OrgName":
						organisations = isAscending ? organisations.OrderBy(a => a.OrgName).ToList() : organisations.OrderByDescending(a => a.OrgName).ToList();
						break;
					case "INN":
						organisations = isAscending ? organisations.OrderBy(a => a.INN).ToList() : organisations.OrderByDescending(a => a.INN).ToList();
						break;
					case "KPP":
						organisations = isAscending ? organisations.OrderBy(a => a.KPP).ToList() : organisations.OrderByDescending(a => a.KPP).ToList();
						break;
					case "AdressReg":
						organisations = isAscending ? organisations.OrderBy(a => a.AdressReg).ToList() : organisations.OrderByDescending(a => a.AdressReg).ToList();
						break;
					case "TypeOrg":
						organisations = isAscending ? organisations.OrderBy(a => a.TypeOrg).ToList() : organisations.OrderByDescending(a => a.TypeOrg).ToList();
						break;
					case "OrgAttribute":
						organisations = isAscending ? organisations.OrderBy(a => a.OrgAttribute).ToList() : organisations.OrderByDescending(a => a.OrgAttribute).ToList();
						break;
					case "Locality":
						organisations = isAscending ? organisations.OrderBy(a => a.Locality).ToList() : organisations.OrderByDescending(a => a.Locality).ToList();
						break;
				}
			}
        
			// Пагинация
			organisations = organisations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        
			return organisations;
		}
        
        
		public static bool ChangeEntry(Organisation organisation)
		{
			bool status = OrganisationsRepository.ChangeEntry(organisation);
			return status;
		}
		
		public static int GetTotalOrganisations(string? filterField, string? filterValue)
		{
			filterValue = filterValue?.ToLower();
			
			var organisations = OrganisationsRepository.GetOrganizations();

			// Применение фильтрации в зависимости от поля
			if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
			{
				switch (filterField.ToLower())
				{
					case "orgname":
						organisations = organisations.Where(o => o.OrgName.ToLower().Contains(filterValue)).ToList();
						break;
					case "inn":
						organisations = organisations.Where(o => o.INN.ToString().Contains(filterValue)).ToList();
						break;
					case "kpp":
						organisations = organisations.Where(o => o.KPP.ToString().Contains(filterValue)).ToList();
						break;
					// Добавьте остальные варианты полей
					default:
						break;
				}
			}

			return organisations.Count;
		}


        public OrganisationService()
		{
			
		}
	}
}

