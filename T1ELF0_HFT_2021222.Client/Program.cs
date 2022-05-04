using ConsoleTools;
using System;

namespace T1ELF0_HFT_2021222.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

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
				.Add("Exit", ConsoleMenu.Close);

			menu.Show();
		}

		private static void Delete(string v)
		{
			switch (v)
			{
				case "car":

					break;
				case "brand":
					break;
				case "rental":
					break;
				default:
					break;
			}
		}

		private static void Update(string v)
		{
			throw new NotImplementedException();
		}

		private static void Read(string v)
		{
			throw new NotImplementedException();
		}

		private static void Create(string v)
		{
			throw new NotImplementedException();
		}

		private static void List(string v)
		{
			throw new NotImplementedException();
		}
	}
}
