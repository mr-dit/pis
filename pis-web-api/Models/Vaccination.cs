using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace pis.Models;

public class Vaccination
{
    [Key]
    public int IdVactination { get; set; }

    public int AnimalId { get; set; }
    public Animal Animal { get; set; }

    public DateTime VaccinationDate { get; set; }
    public int VaccinePriceListByLocalityId { get; set; }
    public VaccinePriceListByLocality VaccinePriceListByLocality { get; set; }

    public int DoctorId { get; set; }
    public User Doctor { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; }

    public Vaccination() { }

    //public Vaccination(int idVactination, Animal animal, DateTime vaccinationDate, VaccinePriceListByLocality vaccinePriceListByLocality, User doctor, Contract contract)
    //{
    //    IdVactination = idVactination;
    //    Animal = animal;
    //    VaccinationDate = vaccinationDate;
    //    VaccinePriceListByLocality = vaccinePriceListByLocality;
    //    Doctor = doctor;
    //    Contract = contract;
    //}
}