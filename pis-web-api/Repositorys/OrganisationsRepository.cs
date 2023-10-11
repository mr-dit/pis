using System;
using pis_web_api.Models;

namespace pis_web_api.Repositorys
{
    public class OrganisationsRepository
    {
        private static List<Organisation> org = new List<Organisation>
        {
            new Organisation(1, "Без Вшей", 1234567890, 987654321, "Примерная, 123", "OOO", "Юр.лицо", "Москва"),
            new Organisation(2, "ЧИП", 987654321, 123456789, "Основная улица, 456", "ИП", "Юр.лицо", "Омск"),
            new Organisation(3, "Братья наши меньшие", 555555555, 111111111, "Центральная площадь, 789", "ООО",
                "Юр.лицо", "Ишим"),
            new Organisation(4, "Без Бродяжек", 777777777, 222222222, "Перспективный проспект, 5", "ООО", "Юр.лицо",
                "Тюмень"),
            new Organisation(5, "Вет Свобода", 12345678901, 333333333, "Innovation Avenue, 100", "ПАО", "Юр.лицо",
                "Тюмень"),
            new Organisation(6, "Без Вшей", 1234567890, 987654321, "Примерная, 123", "OOO", "Юр.лицо", "Москва"),
            new Organisation(7, "ЧИП", 987654321, 123456789, "Основная улица, 456", "ИП", "Юр.лицо", "Омск"),
            new Organisation(8, "Братья наши меньшие", 555555555, 111111111, "Центральная площадь, 789", "ООО",
                "Юр.лицо", "Ишим"),
            new Organisation(9, "Без Бродяжек", 777777777, 222222222, "Перспективный проспект, 5", "ООО", "Юр.лицо",
                "Тюмень"),
            new Organisation(10, "ВетОрг", 1231231231, 12333333, "Innovation Avenue, 100", "ПАО", "Юр.лицо",
                "Екатеринбург"),
            new Organisation(11, "Без Вшей", 1234567890, 987654321, "Примерная, 123", "OOO", "Юр.лицо", "Москва"),
            new Organisation(12, "ЧИП", 987654321, 123456789, "Основная улица, 456", "ИП", "Юр.лицо", "Омск"),
            new Organisation(13, "Братья наши меньшие", 555555555, 111111111, "Центральная площадь, 789", "ООО",
                "Юр.лицо", "Ишим"),
            new Organisation(14, "Без Бродяжек", 777777777, 222222222, "Перспективный проспект, 5", "ООО", "Юр.лицо",
                "Тюмень"),
            new Organisation(15, "Свобода", 12345678901, 333333333, "Innovation Avenue, 100", "ПАО", "Юр.лицо",
                "Тобольск")
        };

        public static bool NewEntry(Organisation organisation)
        {
            try
            {
                int maxRegistrationNumber = org.Max(a => a.OrgId);
                int nextRegistrationNumber = maxRegistrationNumber + 1;

                organisation.OrgId = nextRegistrationNumber;
                org.Add(organisation);
            }
            catch
            {
                return false;
            }

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

            Console.WriteLine("Объект Organisation не найден.");
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
                foundOrg.OrgName = organisation.OrgName;
                foundOrg.INN = organisation.INN;
                foundOrg.KPP = organisation.KPP;
                foundOrg.AdressReg = organisation.AdressReg;
                foundOrg.TypeOrg = organisation.TypeOrg;
                foundOrg.OrgAttribute = organisation.OrgAttribute;
                foundOrg.Locality = organisation.Locality;
                return true;
            }

            return false;
        }


        public OrganisationsRepository()
        {
        }
    }
}