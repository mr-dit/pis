namespace pis.Models;

public class Vaccination
{
    public int VaccineId { get; set; }
    public Animal? Animal { get; set; } 
    public DateTime VaccinationDate { get; set; } 
    public string VaccineType { get; set; } 
    public string BatchNumber { get; set; } 
    public DateTime ValidUntil { get; set; } 
    public string VeterinarianFullName { get; set; } 
    public string VeterinarianPosition { get; set; } 
    public Organisation? Organisation { get; set; } 
    // public Contract MunicipalContract { get; set; }
    
    public Vaccination(int vaccineId, Animal? animal, DateTime vaccinationDate, string vaccineType, string batchNumber, DateTime validUntil, string veterinarianFullName, string veterinarianPosition, Organisation? organisation)
    {
        VaccineId = vaccineId;
        Animal = animal;
        VaccinationDate = vaccinationDate;
        VaccineType = vaccineType;
        BatchNumber = batchNumber;
        ValidUntil = validUntil;
        VeterinarianFullName = veterinarianFullName;
        VeterinarianPosition = veterinarianPosition;
        Organisation = organisation;
    }
    
    public Vaccination()
    {
    }
    
}