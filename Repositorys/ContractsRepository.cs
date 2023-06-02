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
        private static List<Contracts> contracts = new List<Contracts>
        {
            new Contracts(1,new DateTime(2023,2,28), new DateTime(2024,2,28),"Исполнитель 1", "Заказчик 1"),
            new Contracts(2, new DateTime(2023,1,15), new DateTime(2023,1,31), "Исполнитель 2", "Заказчик 2"),
            new Contracts(3, new DateTime(2023,3,10), new DateTime(2024,3,10), "Исполнитель 3", "Заказчик 3")
        };

        public static bool CreateContracts(Contracts newcontracts)
        {
            contracts.Add(newcontracts);
            return true;
        }
        public static bool DeleteEntry(int id)
        {
            var foundContr = contracts.FirstOrDefault(a => a.ContractsId == id);
            if (foundContr != null)
            {
                contracts.Remove(foundContr);
                Console.WriteLine("Объект Contracts удален.");
                return true;
            }
            else
            {
                Console.WriteLine("Объект Contracts не найден.");
            }
            return false;
        }

        public static Contracts? GetEntry(int id)
        {
            var foundContr = contracts.FirstOrDefault(a => a.ContractsId == id);
            return foundContr;
        }

        public static bool ChangeEntry(Contracts newcontracts)
        {
            var foundContr = contracts.FirstOrDefault(a => a.ContractsId == newcontracts.ContractsId);
            if (foundContr != null)
            {
                foundContr = newcontracts;

                contracts.RemoveAll(a => a.ContractsId == newcontracts.ContractsId);
                contracts.Add(foundContr);
                return true;
            }
            return false;
        }

        public static List<Contracts> GetContracts()
        {
            var foundContr = contracts;
            return foundContr;
        }

    }



