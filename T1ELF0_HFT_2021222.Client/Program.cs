using System;
using T1ELF0_HFT_2021222.Logic;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			CarRentalDbContext ctx = new CarRentalDbContext();

			CarRepository carRepo = new CarRepository(ctx);
			BrandRepository brandRepo = new BrandRepository(ctx);
			RentalRepository rentalRepo = new RentalRepository(ctx);

			CarLogic carLogic = new CarLogic(carRepo, brandRepo);
			BrandLogic brandLogic = new BrandLogic(brandRepo, carRepo);
			RentalLogic rentalLogic = new RentalLogic(rentalRepo, carRepo, brandRepo);

			var q = brandLogic.AVGByBrand();
			var q2 = rentalLogic.RentCountByBrand();
			var q3 = carLogic.CountByBrand();
			var q4 = rentalLogic.RentedAfterMarch();
			var q5 = rentalLogic.MostPopular();
		}
	}
}
