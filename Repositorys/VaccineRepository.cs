using NUnit.Framework;
using pis.Models;
using pis.Services;

namespace pis.Repositorys
{
    public class VaccineRepository
    {
        private static List<Vaccine> vaccines = new List<Vaccine>
        {
            new Vaccine (1, "Бешенный", 20),
            new Vaccine (2, "Блошинка", 90)
        };

        public static Vaccine GetVaccineByName(string name)
        {
            var vaccine = vaccines.Where(vaccine => vaccine.NameVaccine == name).FirstOrDefault();
            if (vaccine == null)
                throw new ArgumentException($"Нет вакцины с названием \"{name}\"");
            return vaccine;
        }
    }
}
