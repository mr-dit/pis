using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Drawing;
using pis_web_api.References;

namespace pis_web_api.Models.db;

public class Vaccination : IJurnable
{
    [Key]
    public int IdVactination { get; set; }
    public DateOnly VaccinationDate { get; set; }
    public DateOnly VaccinationValidDate { get; set; }
    public string VaccineSeriesNumber { get; set; }

    public int AnimalId { get; set; }
    public Animal Animal { get; set; }

    public int VaccineId { get; set; }
    public Vaccine? Vaccine { get; set; }

    public int DoctorId { get; set; }
    public User? Doctor { get; set; }

    public int ContractId { get; set; }
    public Contract? Contract { get; set; }

    public int Id => IdVactination;

    public static TableNames TableName { get => TableNames.Вакцинации; }

    public Vaccination() { }

    public Vaccination(string vaccineSeriesNumber, Animal animal, Vaccine vaccine, User doctor, Contract contract)
    {
        VaccineSeriesNumber = vaccineSeriesNumber;
        VaccinationDate = DateOnly.FromDateTime(DateTime.Today);
        VaccinationValidDate = DateOnly.FromDateTime(DateTime.Today.AddDays(vaccine.ValidDaysVaccine));
        AnimalId = animal.RegistrationNumber;
        VaccineId = vaccine.IdVaccine;
        DoctorId = doctor.IdUser;
        ContractId = contract.IdContract;
    }

    public override string ToString()
    {
        var desc = $"{VaccinationDate};" +
            $"{VaccinationValidDate};" +
            $"{VaccineSeriesNumber};" +
            $"{Animal.AnimalName}" +
            $"{Vaccine.NameVaccine};" +
            $"{Contract.Id}";
        return desc;
    }
}