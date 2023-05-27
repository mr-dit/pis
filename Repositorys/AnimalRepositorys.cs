using System;
using pis.Models;
namespace pis.Repositorys
{
	public class AnimalRepositorys
	{

		public static List<Animal> animals = new List<Animal>
		{
			new Animal(1, "Omsk", "Cat", "Boy", 2020, 1, "kot", "photo", "tail"),
            new Animal(2, "Tyumen", "Cat", "Girl", 2022, 2, "kot", "photo", "tail"),
            new Animal(3, "Omsk", "Dog", "Boy", 2021, 3, "kot", "photo", "tail")
        };


		public static bool NewEntry(Animal animal)
		{
            animals.Add(animal);
			return true;
        }

		public AnimalRepositorys()
		{
		}
	}
}

