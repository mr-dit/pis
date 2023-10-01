using System.Diagnostics.Contracts;
using System.Drawing;

namespace pis.Models;

public class Vaccination
{
    public int IdVactination { get; set; }
    public Animal Animal { get; set; }
    public DateTime VaccinationDate { get; set; }
    public VaccinePriceListByLocality VaccinePriceListByLocality { get; set; }
    public Vaccine Vaccine { get { return VaccinePriceListByLocality.Vaccine; } }
    public User Doctor { get; set; }
    public Organisation Vetclinic { get { return Doctor.Organisation; } }
    public Contract Contract { get; set; }

    public Vaccination(int idVactination, Animal animal, DateTime vaccinationDate, VaccinePriceListByLocality vaccinePriceListByLocality, User doctor, Contract contract)
    {
        IdVactination = idVactination;
        Animal = animal;
        VaccinationDate = vaccinationDate;
        VaccinePriceListByLocality = vaccinePriceListByLocality;
        Doctor = doctor;
        Contract = contract;
    }
}