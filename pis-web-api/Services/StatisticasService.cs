﻿using System;
using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis.Services
{
    public class StatisticaService
	{
        public static void /*List<StatisticsItem>*/ GetStatistics(DateTime startDate, DateTime endDate)
        {
            //var vaccines = VaccinationRepository.GetVaccines();
            //// Фильтрация записей вакцинации по указанным параметрам фильтра
            //var filteredVaccinations = vaccines.Where(v => FilterVaccination(v, startDate, endDate));

            //// Группировка записей по населённым пунктам
            //var groupedByLocality = filteredVaccinations.GroupBy(v => v.Animal?.Locality)
            //                                           .Select(g => new StatisticsItem
            //                                           {
            //                                               Locality = g.Key,
            //                                               TotalVaccines = g.Count(),
            //                                               TotalCost = g.Sum(v => CalculateVaccinationCost(v))                                                           
            //                                           })
            //                                           .ToList();

            //return groupedByLocality;
        }

        private static bool FilterVaccination(Vaccination vaccination, DateOnly startDate, DateOnly endDate)
        {
            // Примените фильтрацию по датам вакцинации
            return vaccination.VaccinationDate >= startDate && vaccination.VaccinationDate <= endDate;
        }

        private static decimal CalculateVaccinationCost(Vaccination vaccination)
        {

           // Пример: просто возвращаем фиксированную стоимость 100
            return 100;
        }

        
        public StatisticaService()
		{
		}
	}
}

