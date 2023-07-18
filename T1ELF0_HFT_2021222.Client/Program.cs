using ConsoleTools;
using System;
using System.Collections.Generic;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Client
{
	class Program
	{
		internal class RentsByBrand
		{
			public int Count { get; set; }
			public string Name { get; set; }
		}

		internal class BrandCount
		{
			public string Name { get; set; }
			public int Count { get; set; }
		}

		internal class BrandAVG
		{
			public string Name { get; set; }
			public double AVG { get; set; }
		}

		static RestService rest;

		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			rest = new RestService("http://localhost:42910/", "car");

			var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
				.Add("AVG by brands", () => AVGByBrand())
				.Add("Count by brands", () => CountByBrand())
				.Add("Rental count by brands", () => RentCountByBrand())
				.Add("Rented after March", () => RentedAfterMarch())
				.Add("Most popular car", () => MostPopular())
				.Add("Exit", ConsoleMenu.Close);

			var rentalSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("rental"))
				.Add("Create", () => Create("rental"))
				.Add("Read", () => Read("rental"))
				.Add("Update", () => Update("rental"))
				.Add("Delete", () => Delete("rental"))
				.Add("Exit", ConsoleMenu.Close);

			var brandSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("brand"))
				.Add("Create", () => Create("brand"))
				.Add("Read", () => Read("brand"))
				.Add("Update", () => Update("brand"))
				.Add("Delete", () => Delete("brand"))
				.Add("Exit", ConsoleMenu.Close);

			var carSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("car"))
				.Add("Create", () => Create("car"))
				.Add("Read", () => Read("car"))
				.Add("Update", () => Update("car"))
				.Add("Delete", ()=> Delete("car"))
				.Add("Exit", ConsoleMenu.Close);

			var menu = new ConsoleMenu(args, level: 0)
				.Add("Cars", () => carSubMenu.Show())
				.Add("Brand", () => brandSubMenu.Show())
				.Add("Rentals", () => rentalSubMenu.Show())
				.Add("Querries", () => nonCrudSubMenu.Show())
				.Add("Exit", ConsoleMenu.Close);

			menu.Show();
		}

		private static void MostPopular()
		{
			Car car = rest.Get<Car>("MostPopular", "rental");
			Console.WriteLine($"Most popular car: \nBrandID: {car.BrandId}, Model: {car.Model}, Age: {car.Age}, Price: {car.Price}");
			Console.ReadLine();
		}

		private static void RentedAfterMarch()
		{
			var cars = rest.Get<IEnumerable<Car>>("RentedAfterMarch", "rental");
			Console.WriteLine("Cars rented after March");
			foreach (var car in cars)
			{
				Console.WriteLine($"CarID: {car.Id}, BrandID: {car.BrandId}, Model: {car.Model}, Age: {car.Age}, Price: {car.Price}");
			}
			Console.ReadLine();
		}

		private static void RentCountByBrand()
		{
			var brands = rest.Get<IEnumerable<RentsByBrand>>("RentCountByBrand", "rental");
			Console.WriteLine("Rent counts by brand: ");
			foreach (var brand in brands)
			{
				Console.WriteLine($"Brand: {brand.Name}, Count: {brand.Count}");
			}
			Console.ReadLine();
		}

		private static void CountByBrand()
		{
			var brands = rest.Get<IEnumerable<BrandCount>>("CountByBrand", "car");
			Console.WriteLine("Car counts by brand: ");
			foreach (var brand in brands)
			{
				Console.WriteLine($"Brand: {brand.Name}, Count: {brand.Count}");
			}
			Console.ReadLine();
		}

		private static void AVGByBrand()
		{
			var brands = rest.Get<IEnumerable<BrandAVG>>("AVGByBrand", "brand");
			Console.WriteLine("Average price per brand: ");
			foreach (var brand in brands)
			{
				Console.WriteLine($"Brand: {brand.Name}, Count: {brand.AVG}");
			}
			Console.ReadLine();
		}

		private static void Delete(string v)
		{
			switch (v)
			{
				case "car":
					Console.Write("ID: ");
					int id = int.Parse(Console.ReadLine());

					rest.Delete(id, "car");

					Console.WriteLine($"{id} deleted");
					Console.ReadLine();
					break;
				case "brand":
					Console.Write("ID: ");
					id = int.Parse(Console.ReadLine());

					rest.Delete(id, "brand");

					Console.WriteLine($"{id} deleted");
					Console.ReadLine();
					break;
				case "rental":
					Console.Write("ID: ");
					id = int.Parse(Console.ReadLine());

					rest.Delete(id, "rental");

					Console.WriteLine($"{id} deleted");
					Console.ReadLine();
					break;
				default:
					break;
			}
		}

		private static void Update(string v)
		{
			switch (v)
			{
				case "car":
					Console.Write("ID: ");
					int id = int.Parse(Console.ReadLine());
					Console.Write("Brand ID: ");
					int brandId = int.Parse(Console.ReadLine());
					Console.Write("Model: ");
					string model = Console.ReadLine();
					Console.Write("Age: ");
					int age = int.Parse(Console.ReadLine());
					Console.Write("Price: ");
					int price = int.Parse(Console.ReadLine());

					Car car = rest.Get<Car>(id, "car");

					car.BrandId = brandId;
					car.Model = model;
					car.Age = age;
					car.Price = price;

					rest.Post(car, "car");
					break;
				case "brand":
					Console.Write("ID: ");
					id = int.Parse(Console.ReadLine());
					Console.Write("Name: ");
					string name = Console.ReadLine();

					Brand brand= rest.Get<Brand>(id, "brand");

					brand.Name = name;

					rest.Post(brand, "brand");
					break;
				case "rental":
					Console.Write("ID: ");
					id = int.Parse(Console.ReadLine());
					Console.Write("Car ID: ");
					int carId = int.Parse(Console.ReadLine());
					Console.Write("Date (YYY.MM.DD): ");
					string date = Console.ReadLine();

					Rental rental = rest.Get<Rental>(id, "rental");

					rental.CarId = carId;
					rental.Date = date;

					rest.Post(rental, "rental");
					break;
				default:
					break;
			}
		}

		private static void Read(string v)
		{
			switch (v)
			{
				case "car":
					Console.Write("ID: ");
					int carId = int.Parse(Console.ReadLine());
					
					Car car = rest.Get<Car>(carId, "car");

					Console.WriteLine($"ID: {car.Id}, BrandID: {car.BrandId}, Age: {car.Age}, Price: {car.Price}");
					Console.ReadLine();
					break;
				case "brand":
					Console.Write("ID: ");
					int brandId = int.Parse(Console.ReadLine());

					Brand brand = rest.Get<Brand>(brandId, "brand");

					Console.WriteLine($"ID: {brand.Id}, Name: {brand.Name}");
					Console.ReadLine();
					break;
				case "rental":
					Console.Write("ID: ");
					int rentalId = int.Parse(Console.ReadLine());

					Rental rental = rest.Get<Rental>(rentalId, "rental");

					Console.WriteLine($"ID: {rental.Id}, CarID: {rental.CarId}, Date: {rental.Date}");
					Console.ReadLine();
					break;
				default:
					break;
			}
		}

		private static void Create(string v)
		{
			switch (v)
			{
				case "car":
					Console.Write("Brand ID: ");
					int brandId = int.Parse(Console.ReadLine());
					Console.Write("Model: ");
					string model = Console.ReadLine();
					Console.Write("Age: ");
					int age = int.Parse(Console.ReadLine());
					Console.Write("Price: ");
					int price = int.Parse(Console.ReadLine());

					rest.Post(new Car() { BrandId = brandId, Model = model, Age = age, Price = price}, "car");
					break;
				case "brand":
					Console.Write("Name: ");
					string name = Console.ReadLine();

					rest.Post(new Brand() { Name = name },"brand");
					break;
				case "rental":
					Console.Write("Car ID: ");
					int carId = int.Parse(Console.ReadLine());
					Console.Write("Date (YYY.MM.DD): ");
					string date = Console.ReadLine();
					
					rest.Post(new Rental() { CarId = carId, Date = date }, "rental");
					break;
				default:
					break;
			}
		}

		private static void List(string v)
		{
			switch (v)
			{
				case "car":
					List<Car> cars = rest.Get<Car>("car");
					Console.WriteLine("Cars:");
					foreach (var car in cars)
					{
						Console.WriteLine($"ID: {car.Id}, BrandID: {car.BrandId}, Age: {car.Age}, Price: {car.Price}");
					}
					Console.ReadLine();
					break;
				case "brand":
					List<Brand> brands = rest.Get<Brand>("brand");
					Console.WriteLine("Cars:");
					foreach (var brand in brands)
					{
						Console.WriteLine($"ID: {brand.Id}, Name: {brand.Name}");
					}
					Console.ReadLine();
					break;
				case "rental":
					List<Rental> rentals= rest.Get<Rental>("rental");
					Console.WriteLine("Cars:");
					foreach (var rental in rentals)
					{
						Console.WriteLine($"ID: {rental.Id}, CarID: {rental.CarId}, Date: {rental.Date}");
					}
					Console.ReadLine();
					break;
				default:
					break;
			}
		}
	}
}
