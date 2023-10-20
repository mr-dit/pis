using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Services;
using pis.Repositorys;
using pis;
using Microsoft.EntityFrameworkCore;

public class ContractsRepository
{
    public static bool CreateContract(Contract contract)
    {
        using (var db = new Context())
        {
            try
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

    public static bool UpdateContract(Contract con)
    {
        using (var db = new Context())
        {
            try
            {
                db.Contracts.Update(con);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

    public static bool DeleteContract(Contract con)
    {
        using (var db = new Context())
        {
            try
            {
                db.Contracts.Remove(con);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

    public static Contract GetContractById(int id)
    {
        using(var db = new Context())
        {
            var con = db.Contracts
                .Where(con => con.IdContract == id)
                .Include(x => x.Performer)
                .Include(x => x.Customer)
                .Include(x => x.Vaccinations)
                .Single();
            if (con == null)
                throw new ArgumentException($"Нет контракта с номером {id}");
            return con;
        }
    }

    public static IQueryable<Contract> GetContractsByOrganisationName(string orgName)
    {
        using (var db = new Context())
        {
            var cons = db.Contracts
                .Include(con => con.Customer)
                .Include(con => con.Performer)
                .Where(con => con.Customer.OrgName == orgName || con.Performer.OrgName == orgName);
            if (cons.Count() == 0)
                throw new ArgumentException($"Не существует контрактов с организацией {orgName}");
            return cons;
        }
    }

    public static IQueryable<Contract> GetContractsByDate(DateOnly date)
    {
        using (var db = new Context())
        {
            var cons = db.Contracts
                .Where(con => date == con.ConclusionDate);
            if (cons.Count() == 0)
                throw new ArgumentException($"Не существует контрактов {date.ToShortDateString()}");
            return cons;
        }
    }

    // Переделать чтоб работало с 2 датами, а не с одной
    public static IQueryable<Contract> GetContractsByDate(DateOnly fromDate, DateOnly toDate)
    {
        using (var db = new Context())
        {
            var cons = db.Contracts
                .Where(con => fromDate < con.ConclusionDate || toDate > con.ConclusionDate);
            //if (cons.Count() == 0)
            //    throw new ArgumentException($"Не существует контрактов в периоде дат {fromDate.ToShortDateString()} - {toDate.ToShortDateString()}");
            return cons;
        }
    }
}