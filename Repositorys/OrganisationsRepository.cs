using System;
using pis.Models;

namespace pis.Repositorys
{
	public class OrganisationsRepository
	{
        public static List<Organisation> org = new List<Organisation>
        {
            new Organisation("Без Вшей", 1234567890, 987654321, "Примерная, 123", "OOO", "Юр.лицо", "Москва"),
            new Organisation("ЧИП", 987654321, 123456789, "Основная улица, 456", "ИП", "Юр.лицо", "Тобольск"),
            new Organisation("Братья наши меньшие", 555555555, 111111111, "Центральная площадь, 789", "ООО", "Юр.лицо", "Ишим"),
            new Organisation("Без Бродяжек", 777777777, 222222222, "Перспективный проспект, 5", "ООО", "Юр.лицо", "Тюмень"),
            new Organisation("Вет Свобода", 888888888, 333333333, "Innovation Avenue, 100", "ПАО", "Юр.лицо", "Тюмень")

        };
        public OrganisationsRepository()
        {
        }
    }
}

