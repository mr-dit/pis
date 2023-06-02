using System;
using pis.Models;

namespace pis.Repositorys
{
	public class OrganisationsRepository
	{
        private static List<Organisation> org = new List<Organisation>
        {
            new Organisation(1, "Без Вшей", 1234567890, 987654321, "Примерная, 123", "OOO", "Юр.лицо", "Москва"),
            new Organisation(2, "ЧИП", 987654321, 123456789, "Основная улица, 456", "ИП", "Юр.лицо", "Тобольск"),
            new Organisation(3, "Братья наши меньшие", 555555555, 111111111, "Центральная площадь, 789", "ООО", "Юр.лицо", "Ишим"),
            new Organisation(4, "Без Бродяжек", 777777777, 222222222, "Перспективный проспект, 5", "ООО", "Юр.лицо", "Тюмень"),
            new Organisation(5, "Вет Свобода", 12345678901, 333333333, "Innovation Avenue, 100", "ПАО", "Юр.лицо", "Тюмень")

        };
        
        public static bool NewEntry(Organisation organisation)
        {
            org.Add(organisation);
            return true;
        }

        public static bool DeleteEntry(int id)
        {
            var foundOrg = org.FirstOrDefault(a => a.OrgId == id);
            if (foundOrg != null)
            {
                org.Remove(foundOrg);
                Console.WriteLine("Объект Organisation удален.");
                return true;
            }
            else
            {
                Console.WriteLine("Объект Organisation не найден.");
            }
            return false;
        }

        public static Organisation? GetEntry(int id)
        {
            var foundOrg = org.FirstOrDefault(a => a.OrgId == id);
            return foundOrg;
        }
        
        public static List<Organisation> GetOrganizations()
        {
            var foundOrg = org;
            return foundOrg;
        }
        
        public static bool ChangeEntry(Organisation organisation)
        {
            var foundOrg = org.FirstOrDefault(a => a.OrgId == organisation.OrgId);
            if (foundOrg != null)
            {
                foundOrg = organisation;
		        
                org.RemoveAll(a => a.OrgId == organisation.OrgId);
                org.Add(foundOrg);
                return true;
            }
            return false;
        }
        
        
        public OrganisationsRepository()
        {
        }
    }
}

