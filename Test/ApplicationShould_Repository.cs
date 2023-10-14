using NUnit.Framework;
using pis.Models;
using pis.Repositorys;

namespace pis.Test
{
    [TestFixture]
    public class ApplicationShould_Repository
    {
        [OneTimeSetUp]
        public void Init()
        {
            InitAnimalCategories();
            InitLocality();
            InitGenders();
            InitOrgTypes();
            InitPosts();
            InitVaccine();
            InitAnimals();
        }

        [Test]
        public static void GetAnimalCategory()
        {
            var categoryFromDbDog = AnimalCategoryRepository.GetAnimalCategoryByName("Собака");
            var categoryFromDbCat = AnimalCategoryRepository.CAT;

            Assert.IsTrue(categoryFromDbCat.NameAnimalCategory == "Кот");
            Assert.IsTrue(categoryFromDbDog.NameAnimalCategory == "Собака");
        }

        [Test]
        public static void GetLocality()
        {
            var tyumenFromDb = LocalityRepository.GetLocalityByName("Тюмень");
            var zubarevaFromDb = LocalityRepository.GetLocalityById(2);

            Assert.IsTrue(tyumenFromDb.NameLocality == "Тюмень");
            Assert.IsTrue(zubarevaFromDb.NameLocality == "Зубарева");
        }

        [Test]
        public static void GetGender()
        {
            var maleFromDb = GenderRepository.GetGenderByName("Мужской");
            var femaleFromDb = GenderRepository.FEMALE;

            Assert.IsTrue(maleFromDb.NameGender == "Мужской");
            Assert.IsTrue(femaleFromDb.NameGender == "Женский");
        }

        [Test]
        public static void GetOrgType()
        {
            var zayavitelFromDb = OrgTypeRepository.GetOrgTypeByName("Заявитель");
            var shelterFromDb = OrgTypeRepository.SHELTER;

            Assert.IsTrue(zayavitelFromDb.NameOrgType == "Заявитель");
            Assert.IsTrue(shelterFromDb.NameOrgType == "Приют");
        }

        [Test]
        public static void GetPosts()
        {
            var doctorFromDb = PostRepository.GetPostByName("Ветврач приюта");
            var kuratorOMSUFromDb = PostRepository.KURATOR_OMSU;

            Assert.IsTrue(doctorFromDb.NamePost == "Ветврач приюта");
            Assert.IsTrue(kuratorOMSUFromDb.NamePost == "Куратор ОМСУ");
        }

        [Test] 
        public static void GetVaccine()
        {
            var vaccine1FromDb = VaccineRepository.GetVaccineByName("Блошинка");
            var vaccine2FromDb = VaccineRepository.GetVaccineByName("Бешенство");

            Assert.IsTrue(vaccine1FromDb.NameVaccine == "Блошинка" && vaccine1FromDb.ValidDaysVaccine == 90);
            Assert.IsTrue(vaccine2FromDb.NameVaccine == "Бешенство" && vaccine2FromDb.ValidDaysVaccine == 180);
        }

        [Test]
        public static void GetAnimal()
        {
            var animal1Db = AnimalRepository.GetAnimalByChipNumber("782872782");
            var animal2Db = AnimalRepository.GetAnimalsByName("Шарик");
            var twoAnimalByName = AnimalRepository.GetAnimalsByName("Шар");
            var animalsByCategory = AnimalRepository.GetAnimalsByAnimalCategory(AnimalCategoryRepository.CAT);

            Assert.IsTrue(animal1Db.ElectronicChipNumber == "782872782" && animal1Db.AnimalCategory.NameAnimalCategory == "Кот");
            Assert.IsTrue(animal2Db.Count() == 1 && animal2Db[0].AnimalName == "Шарик");
            Assert.IsTrue(twoAnimalByName.Count() == 2);
            Assert.IsTrue(animalsByCategory.Count() == 2);
        }

        // Inits
        private void InitAnimals()
        {
            var animal1 = new Animal("Барсик", LocalityRepository.GetLocalityByName("Тюмень").IdLocality,
                AnimalCategoryRepository.CAT.IdAnimalCategory, GenderRepository.MALE.IdGender, 2020, "782872782");
            var animal2 = new Animal("Шарик", LocalityRepository.GetLocalityByName("Тюмень").IdLocality,
                AnimalCategoryRepository.DOG.IdAnimalCategory, GenderRepository.MALE.IdGender, 2020, "292929292");
            var animal3 = new Animal("Тубзик", LocalityRepository.GetLocalityByName("Тюмень").IdLocality,
                AnimalCategoryRepository.DOG.IdAnimalCategory, GenderRepository.MALE.IdGender, 2019, "323729329");
            var animal4 = new Animal("Шаризя", LocalityRepository.GetLocalityByName("Зубарева").IdLocality,
                AnimalCategoryRepository.CAT.IdAnimalCategory, GenderRepository.MALE.IdGender, 2021, "101023923");

            AnimalRepository.CreateAnimal(animal1);
            AnimalRepository.CreateAnimal(animal2);
            AnimalRepository.CreateAnimal(animal3);
            AnimalRepository.CreateAnimal(animal4);
        }

        private void InitVaccine()
        {
            var vaccine1 = new Vaccine("Блошинка", 90);
            var vaccine2 = new Vaccine("Бешенство", 180);

            VaccineRepository.AddVaccine(vaccine1);
            VaccineRepository.AddVaccine(vaccine2);
        }

        private void InitLocality()
        {
            var localityTyumen = new Locality();
            localityTyumen.NameLocality = "Тюмень";

            var localityZubareva = new Locality();
            localityZubareva.NameLocality = "Зубарева";

            LocalityRepository.AddLocality(localityTyumen);
            LocalityRepository.AddLocality(localityZubareva);
        }

        private void InitAnimalCategories()
        {
            var animalCategoryCat = new AnimalCategory();
            animalCategoryCat.NameAnimalCategory = "Кот";

            var animalCategoryDog = new AnimalCategory();
            animalCategoryDog.NameAnimalCategory = "Собака";

            AnimalCategoryRepository.AddAnimalCategory(animalCategoryCat);
            AnimalCategoryRepository.AddAnimalCategory(animalCategoryDog);
        }

        private void InitGenders()
        {
            var male = new Gender();
            male.NameGender = "Мужской";

            var female = new Gender();
            female.NameGender = "Женский";

            GenderRepository.AddGender(male);
            GenderRepository.AddGender(female);
        }

        private void InitOrgTypes()
        {
            var types = new List<OrgType>()
            {
                new OrgType("Исполнительный орган государственной власти"),
                new OrgType("Орган местного самоуправления"),
                new OrgType("Приют"),
                new OrgType("Организация по отлову"),
                new OrgType("Организация по транспортировке"),
                new OrgType("Ветеринарная клиника: государственная"),
                new OrgType("Ветеринарная клиника: муниципальная"),
                new OrgType("Ветеринарная клиника: частная"),
                new OrgType("Благотворительный фонд"),
                new OrgType("Организации по продаже товаров и предоставлению услуг для животных"),
                new OrgType("Организация по отлову и приют"),
                new OrgType("Заявитель")
            };

            foreach (var type in types)
            {
                OrgTypeRepository.AddOrgType(type);
            }

        }

        private void InitPosts()
        {
            var posts = new List<Post>()
            {
                new Post("Куратор ВетСлужбы"),
                new Post("Куратор по отлову"),
                new Post("Куратор приюта"),
                new Post("Оператор ВетСлужбы"),
                new Post("Оператор по отлову"),
                new Post("Подписант ВетСлужбы"),
                new Post("Подписант по отлову"),
                new Post("Подписант приюта"),
                new Post("Куратор ОМСУ"),
                new Post("Оператор ОМСУ"),
                new Post("Подписант ОМСУ"),
                new Post("Оператор приюта"),
                new Post("Ветврач"),
                new Post("Ветврач приюта")
            };

            foreach (var post in posts)
            {
                PostRepository.AddPost(post);
            }
        }
    }
}
