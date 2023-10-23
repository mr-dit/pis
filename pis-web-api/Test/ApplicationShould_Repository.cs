using NUnit.Framework;
using pis.Models;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Models;
using pis_web_api.Services;

namespace pis.Test
{
    [TestFixture]
    public class ApplicationShould_Repository
    {
        [OneTimeSetUp]
        public void Init()
        {
            //InitAnimalCategories();
            //InitLocality();
            //InitGenders();
            //InitOrgTypes();
            //InitRoles();
            //InitVaccine();
            //InitAnimals();
            //InitOrganisations();
            //InitUsers();
            //InitContracts();
            //InitVaccinations();
        }

        //[Test]
        //public static void GetUser()
        //{
        //    var user = new UserService().GetEntry(1);

        //    Assert.IsTrue(user.Roles.Count() == 4);
        //}

        //[Test]
        //public static void GetAnimalCategory()
        //{
        //    var categoryFromDbDog = AnimalCategoryRepository.GetAnimalCategoryByName("Собака");
        //    var categoryFromDbCat = AnimalCategoryRepository.CAT;

        //    Assert.IsTrue(categoryFromDbCat.NameAnimalCategory == "Кот");
        //    Assert.IsTrue(categoryFromDbDog.NameAnimalCategory == "Собака");
        //}

        //[Test]
        //public static void GetLocality()
        //{
        //    var tyumenFromDb = LocalityRepository.GetLocalityByName("Тюмень");
        //    var zubarevaFromDb = LocalityRepository.GetLocalityById(2);

        //    Assert.IsTrue(tyumenFromDb.NameLocality == "Тюмень");
        //    Assert.IsTrue(zubarevaFromDb.NameLocality == "Зубарева");
        //}

        //[Test]
        //public static void GetGender()
        //{
        //    var maleFromDb = GenderRepository.MALE;
        //    var femaleFromDb = GenderRepository.FEMALE;

        //    Assert.IsTrue(maleFromDb.NameGender == "Мужской");
        //    Assert.IsTrue(femaleFromDb.NameGender == "Женский");
        //}

        //[Test]
        //public static void GetOrgType()
        //{
        //    var zayavitelFromDb = OrgTypeRepository.GetOrgTypeByName("Заявитель");
        //    var shelterFromDb = OrgTypeRepository.SHELTER;

        //    Assert.IsTrue(zayavitelFromDb.NameOrgType == "Заявитель");
        //    Assert.IsTrue(shelterFromDb.NameOrgType == "Приют");
        //}

        //[Test]
        //public static void GetPosts()
        //{
        //    var doctorFromDb = RoleRepository.GetRoleByName("Ветврач приюта");
        //    var kuratorOMSUFromDb = RolesService.KURATOR_OMSU;

        //    Assert.IsTrue(doctorFromDb.NameRole == "Ветврач приюта");
        //    Assert.IsTrue(kuratorOMSUFromDb.NameRole == "Куратор ОМСУ");
        //}

        //[Test] 
        //public static void GetVaccine()
        //{
        //    var vaccine1FromDb = VaccineRepository.GetVaccineByName("Блошинка");
        //    var vaccine2FromDb = VaccineRepository.GetVaccineByName("Бешенство");

        //    Assert.IsTrue(vaccine1FromDb.NameVaccine == "Блошинка" && vaccine1FromDb.ValidDaysVaccine == 90);
        //    Assert.IsTrue(vaccine2FromDb.NameVaccine == "Бешенство" && vaccine2FromDb.ValidDaysVaccine == 180);
        //}

        //[Test]
        //public static void GetAnimal()
        //{
        //    var animal1Db = AnimalRepository.GetAnimalByChipNumber("782872782");
        //    var animal2Db = AnimalRepository.GetAnimalsByName("Шарик");
        //    var twoAnimalByName = AnimalRepository.GetAnimalsByName("Шар");
        //    var animalsByCategory = AnimalRepository.GetAnimalsByAnimalCategory(AnimalCategoryRepository.CAT);

        //    Assert.IsTrue(animal1Db.ElectronicChipNumber == "782872782" && animal1Db.AnimalCategory.NameAnimalCategory == "Кот");
        //    Assert.IsTrue(animal2Db.Count() == 1 && animal2Db[0].AnimalName == "Шарик");
        //    Assert.IsTrue(twoAnimalByName.Count() == 2);
        //    Assert.IsTrue(animalsByCategory.Count() == 2);
        //}

        //[Test]
        //public static void GetOrganisations()
        //{
        //    var service = new OrganisationService();
        //    var org1Db = service.GetEntry(1);

        //    Assert.IsTrue(org1Db.OrgName == "Клиника добряков");
        //    Assert.IsTrue(org1Db.OrgType.NameOrgType == OrgTypeRepository.GOV_VETCLINIC.NameOrgType);
        //}

        // Inits
        //private void InitVaccinations()
        //{
        //    var vaccinations = new List<Vaccination>()
        //    {
        //        new Vaccination("9823983298", AnimalRepository.GetAnimalById(1), 
        //        VaccineRepository.GetVaccineById(1), UserRepository.GetUserById(1), 
        //        ContractsRepository.GetContractById(1)),

        //        new Vaccination("12312321321", AnimalRepository.GetAnimalById(2),
        //        VaccineRepository.GetVaccineById(2), UserRepository.GetUserById(3),
        //        ContractsRepository.GetContractById(2)),

        //        new Vaccination("3425342542325", AnimalRepository.GetAnimalById(3),
        //        VaccineRepository.GetVaccineById(3), UserRepository.GetUserById(2),
        //        ContractsRepository.GetContractById(4)),

        //        new Vaccination("56758675687", AnimalRepository.GetAnimalById(4),
        //        VaccineRepository.GetVaccineById(1), UserRepository.GetUserById(1),
        //        ContractsRepository.GetContractById(1))
        //    };

        //    foreach (var vaccination in vaccinations)
        //    {
        //        VaccinationRepository.AddVacciantion(vaccination);
        //    }
        //}

        private void InitContracts()
        {
            //var contracts = new List<Contract>()
            //{
            //    new Contract(DateTime.Today.AddDays(365), 
            //    OrganisationsRepository.GetOrganisationById(4), 
            //    OrganisationsRepository.GetOrganisationById(1)),

            //    new Contract(DateTime.Today.AddDays(180),
            //    OrganisationsRepository.GetOrganisationById(4),
            //    OrganisationsRepository.GetOrganisationById(2)),

            //    new Contract(DateTime.Today.AddDays(730),
            //    OrganisationsRepository.GetOrganisationById(4),
            //    OrganisationsRepository.GetOrganisationById(3)),

            //    new Contract(DateTime.Today.AddDays(90),
            //    OrganisationsRepository.GetOrganisationById(4),
            //    OrganisationsRepository.GetOrganisationById(5)),

            //    new Contract(DateTime.Today.AddDays(90),
            //    OrganisationsRepository.GetOrganisationById(5),
            //    OrganisationsRepository.GetOrganisationById(1)),
            //};

            //foreach (var contract in contracts)
            //{
            //    ContractsRepository.CreateContract(contract);
            //}

            //contracts[0].AddLocalitisList(LocalityRepository.GetLocalityById(1), 1000);
            //contracts[0].AddLocalitisList(LocalityRepository.GetLocalityById(2), 1200);
            //contracts[0].AddLocalitisList(LocalityRepository.GetLocalityById(3), 1100);
            //contracts[1].AddLocalitisList(LocalityRepository.GetLocalityById(1), 900);
            //contracts[1].AddLocalitisList(LocalityRepository.GetLocalityById(4), 950);
            //contracts[2].AddLocalitisList(LocalityRepository.GetLocalityById(1), 1999);
            //contracts[2].AddLocalitisList(LocalityRepository.GetLocalityById(2), 2100);
            //contracts[3].AddLocalitisList(LocalityRepository.GetLocalityById(1), 1000);
            //contracts[3].AddLocalitisList(LocalityRepository.GetLocalityById(1), 1000);
            //contracts[4].AddLocalitisList(LocalityRepository.GetLocalityById(1), 1299);
        }

        //private void InitUsers()
        //{
        //    var userService = new UserService();
        //    var users = new List<User>()
        //    {
        //        new User("Веселов", "Михаил", "Константинович", 1),
        //        new User("Теплов", "Ярослав", "Игоревич", 5),
        //        new User("Рудин", "Валентин", "Константинович", 2),
        //        new User("Ширгазина", "Аида", "Владиславовна", 4),
        //        new User("Хорьякова", "Мария", "Дмитриевна", 1),
        //        new User("Мезенцев", "Дмитрий", "Сергеевич", 5),
        //        new User("Харченко", "Ева", "Андреевна", 2),
        //        new User("Абдраман", "Сидик", "Сулейман", 2)
        //    };

        //    foreach (var user in users)
        //    {
        //        userService.FillData(user);
        //    }

        //    users[0].AddRoles(RolesService.DOCTOR, RolesService.KURATOR_VETSERVICE,
        //        RolesService.OPERATOR_VETSERVICE, RolesService.SIGNER_VETSERVICE);
        //    users[1].AddRoles(RolesService.DOCTOR, RolesService.DOCTOR_SHELTER,
        //        RolesService.OPERATOR_SHELTER, RolesService.KURATOR_SHELTER);
        //    users[2].AddRoles(RolesService.DOCTOR, RolesService.KURATOR_VETSERVICE,
        //        RolesService.OPERATOR_VETSERVICE, RolesService.SIGNER_VETSERVICE);
        //    users[3].AddRoles(RolesService.KURATOR_OMSU, RolesService.OPERATOR_OMSU, RolesService.SIGNER_OMSU);
        //    users[4].AddRoles(RolesService.DOCTOR);
        //    users[5].AddRoles(RolesService.DOCTOR_SHELTER);
        //    users[6].AddRoles(RolesService.KURATOR_VETSERVICE);
        //    users[7].AddRoles(RolesService.OPERATOR_VETSERVICE);
        //}

        private void InitOrganisations()
        {
            //var org1 = new Organisation("Клиника добряков", "8282830303", "8173518312",
            //    "Улица Пушкина, Дом Колотушкина", OrgTypeRepository.GOV_VETCLINIC.IdOrgType,
            //    LocalityRepository.GetLocalityByName("Тюмень").IdLocality);
            //var org2 = new Organisation("Клиника злюков", "6666666666", "9999999999",
            //    "Улица Клушкина, Дом 13", OrgTypeRepository.GOV_VETCLINIC.IdOrgType,
            //    LocalityRepository.GetLocalityByName("Тюмень").IdLocality);
            //var org3 = new Organisation("Клиника Зубаревская", "1927842032", "19832678345",
            //    "Улица Победы, Дом 9", OrgTypeRepository.GOV_VETCLINIC.IdOrgType,
            //    LocalityRepository.GetLocalityByName("Зубарева").IdLocality);
            //var org4 = new Organisation("ОМСУ Тюменской обл.", "6666666666", "9999999999",
            //    "Улица Ленин, Дом 1", OrgTypeRepository.OMSU.IdOrgType,
            //    LocalityRepository.GetLocalityByName("Тюмень").IdLocality);
            //var org5 = new Organisation("Приют", "789123123", "190129012",
            //    "Улица Петра, Дом 2", OrgTypeRepository.SHELTER.IdOrgType,
            //    LocalityRepository.GetLocalityByName("Зубарева").IdLocality);

            //OrganisationsRepository.AddOrganisation(org1);
            //OrganisationsRepository.AddOrganisation(org2);
            //OrganisationsRepository.AddOrganisation(org3);
            //OrganisationsRepository.AddOrganisation(org4);
            //OrganisationsRepository.AddOrganisation(org5);
        }

        //private void InitAnimals()
        //{
        //    var animal1 = new Animal("Барсик", LocalityRepository.GetLocalityByName("Тюмень").IdLocality,
        //        AnimalCategoryRepository.CAT.IdAnimalCategory, GenderRepository.MALE.IdGender, 2020, "782872782");
        //    var animal2 = new Animal("Шарик", LocalityRepository.GetLocalityByName("Тюмень").IdLocality,
        //        AnimalCategoryRepository.DOG.IdAnimalCategory, GenderRepository.MALE.IdGender, 2020, "292929292");
        //    var animal3 = new Animal("Тубзик", LocalityRepository.GetLocalityByName("Тюмень").IdLocality,
        //        AnimalCategoryRepository.DOG.IdAnimalCategory, GenderRepository.MALE.IdGender, 2019, "323729329");
        //    var animal4 = new Animal("Шаризя", LocalityRepository.GetLocalityByName("Зубарева").IdLocality,
        //        AnimalCategoryRepository.CAT.IdAnimalCategory, GenderRepository.MALE.IdGender, 2021, "101023923");

        //    AnimalRepository.CreateAnimal(animal1);
        //    AnimalRepository.CreateAnimal(animal2);
        //    AnimalRepository.CreateAnimal(animal3);
        //    AnimalRepository.CreateAnimal(animal4);
        //}

        //private void InitVaccine()
        //{
        //    var vaccines = new List<Vaccine>()
        //    {
        //        new Vaccine("Блошинка", 90),
        //        new Vaccine("Бешенство", 180),
        //        new Vaccine("От всего", 14),
        //        new Vaccine("Плацебло", 365),
        //        new Vaccine("Спутник", 180),
        //        new Vaccine("RISC - V", 666)
        //    };

        //    foreach (var vaccine in vaccines)
        //    {
        //        VaccineRepository.AddVaccine(vaccine);
        //    }
        //}

        //private void InitLocality()
        //{
        //    var localities = new List<Locality>()
        //    {
        //        new Locality("Тюмень"),
        //        new Locality("Зубарева"),
        //        new Locality("Яр"),
        //        new Locality("Ембаево"),
        //        new Locality("Патрушева"),
        //        new Locality("Луговое"),
        //        new Locality("Боровский"),
        //        new Locality("Андреевский"),
        //    };

        //    foreach (var locality in localities)
        //    {
        //        LocalityRepository.AddLocality(locality);
        //    }
        //}

        //private void InitAnimalCategories()
        //{
        //    var animalCategoryCat = new AnimalCategory("Кот");
        //    var animalCategoryDog = new AnimalCategory("Собака");

        //    AnimalCategoryRepository.AddAnimalCategory(animalCategoryCat);
        //    AnimalCategoryRepository.AddAnimalCategory(animalCategoryDog);
        //}

        //private void InitGenders()
        //{
        //    var male = new Gender("Мужской");
        //    var female = new Gender("Женский");

        //    GenderRepository.AddGender(male);
        //    GenderRepository.AddGender(female);
        //}

        //private void InitOrgTypes()
        //{
        //    var types = new List<OrgType>()
        //    {
        //        new OrgType("Исполнительный орган государственной власти"),
        //        new OrgType("Орган местного самоуправления"),
        //        new OrgType("Приют"),
        //        new OrgType("Организация по отлову"),
        //        new OrgType("Организация по транспортировке"),
        //        new OrgType("Ветеринарная клиника: государственная"),
        //        new OrgType("Ветеринарная клиника: муниципальная"),
        //        new OrgType("Ветеринарная клиника: частная"),
        //        new OrgType("Благотворительный фонд"),
        //        new OrgType("Организации по продаже товаров и предоставлению услуг для животных"),
        //        new OrgType("Организация по отлову и приют"),
        //        new OrgType("Заявитель")
        //    };

        //    foreach (var type in types)
        //    {
        //        OrgTypeRepository.AddOrgType(type);
        //    }

        //}

        //private void InitRoles()
        //{
        //    var roles = new List<Role>()
        //    {
        //        new Role("Куратор ВетСлужбы"),
        //        new Role("Куратор по отлову"),
        //        new Role("Куратор приюта"),
        //        new Role("Оператор ВетСлужбы"),
        //        new Role("Оператор по отлову"),
        //        new Role("Подписант ВетСлужбы"),
        //        new Role("Подписант по отлову"),
        //        new Role("Подписант приюта"),
        //        new Role("Куратор ОМСУ"),
        //        new Role("Оператор ОМСУ"),
        //        new Role("Подписант ОМСУ"),
        //        new Role("Оператор приюта"),
        //        new Role("Ветврач"),
        //        new Role("Ветврач приюта")
        //    };

        //    foreach (var role in roles)
        //    {
        //        RoleRepository.AddRole(role);
        //    }
        //}
    }
}
