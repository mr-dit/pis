using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Services;
using pis.Repositorys;

public class ContractsRepository
{
    private static List<Contract> contracts = new List<Contract>
    {
        new Contract(1, "00001", DateTime.Today, DateTime.Today.AddDays(30), 
            OrganisationsRepository.GetOrganisationByName("Дружба"), 
            OrganisationsRepository.GetOrganisationByName("Администрация г. Тюмень"),
            new List<VaccinePriceListByLocality>
                {
                    VaccinePriceListRepository.GetVaccinePriceList("Бешенный", "Тюмень"),
                    VaccinePriceListRepository.GetVaccinePriceList("Бешенный", "Зубарева")
                },
            new List<Vaccination>()
            )
    };

    //public static bool CreateContracts(Contracts newcontracts)
    //{
    //    var org = OrganisationsRepository.GetEntry(newcontracts.Performer.OrgId);
    //    newcontracts.ContractsId = GetContracts().Count() + 1;
    //    newcontracts.Performer = org;
    //    contracts.Add(newcontracts);

    //    return true;
    //}

    //public static bool DeleteEntry(int id)
    //{
    //    var foundContr = contracts.FirstOrDefault(a => a.ContractsId == id);
    //    if (foundContr != null)
    //    {
    //        contracts.Remove(foundContr);
    //        Console.WriteLine("Объект Contracts удален.");
    //        return true;
    //    }

    //    Console.WriteLine("Объект Contracts не найден.");
    //    return false;
    //}

    //public static Contracts? GetEntry(int id)
    //{
    //    var foundContr = contracts.FirstOrDefault(a => a.ContractsId == id);
    //    return foundContr;
    //}

    //public static bool ChangeEntry(Contracts newcontracts)
    //{
    //    var foundContr = contracts.FirstOrDefault(a => a.ContractsId == newcontracts.ContractsId);
    //    if (foundContr != null)
    //    {
    //        var org = OrganisationsRepository.GetEntry(newcontracts.Performer.OrgId);
    //        foundContr.Performer = org;
    //        foundContr.ConclusionDate = newcontracts.ConclusionDate;
    //        foundContr.ExpirationDate = newcontracts.ExpirationDate;
    //        foundContr.Customer = newcontracts.Customer;
    //        return true;
    //    }

    //    return false;
    //}

    //public static List<Contracts> GetContracts()
    //{
    //    var foundContr = contracts;
    //    return foundContr;
    //}
}