using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Models;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Logic
{
	public class RentalLogic
	{
		IRepository<Rental> repo;
		IRepository<Car> carRepo;
		IRepository<Brand> brandRepo;

		public RentalLogic(IRepository<Rental> repo, IRepository<Car> carRepo, IRepository<Brand> brandRepo)
		{
			this.repo = repo;
			this.carRepo = carRepo;
			this.brandRepo = brandRepo;
		}

		public void Create(Rental item)
		{
			this.repo.Create(item);
		}

		public void Delete(int id)
		{
			if (this.repo.Read(id) == null)
			{
				throw new Exception("Item not found");
			}

			this.repo.Delete(id);
		}

		public Rental Read(int id)
		{
			if (this.repo.Read(id) == null)
			{
				throw new Exception("Item not found");
			}

			return this.repo.Read(id);
		}

		public IQueryable<Rental> ReadAll()
		{
			return this.repo.ReadAll();
		}

		public void Update(Rental item)
		{
			if (Read(item.Id) == null)
			{
				throw new Exception("Item not found");
			}

			this.repo.Update(item);
		}

		public IQueryable AVGByRental()
		{
			var q = from rental in repo.ReadAll()
					join car in carRepo.ReadAll()
					on rental.CarId equals car.Id
					select new
					{
						Rental = rental.Id,
						Price = car.Price
					};
			var q2 = from item in q
					 group item by item.Rental into g
					 select new
					 {
						 Brand = g.Key,
						 AVG = q.Where(c => c.Rental == g.Key).Select(c => c.Price).Average()
					 };

			return q2;
		}

		public IQueryable RentCountByBrand()
		{
			var q = from rental in repo.ReadAll()
					join car in carRepo.ReadAll()
					on rental.CarId equals car.Id
					join brand in brandRepo.ReadAll()
					on car.BrandId equals brand.Id
					select new
					{
						Rental = rental.Id,
						Brand = brand.Name
					};
			var q2 = from item in q
					 group item by item.Brand into g
					 select new
					 {
						 Brand = g.Key,
						 Count = q.Where(r => r.Brand == g.Key).Count()
					 };

			return q2;
		}

		public IQueryable RentedAfterMarch()
		{
			var q = from rental in repo.ReadAll()
					join car in carRepo.ReadAll()
					on rental.CarId equals car.Id
					select new
					{
						Date = rental.Date,
						Car = car
					};
			var q2 = q.Where(c => DateTime.Parse(c.Date) >= DateTime.Parse("2022.03.01")).Select(c => c.Car);

			return q2;
		}

		public Car MostPopular()
		{
			var q = from rental in repo.ReadAll()
					join car in carRepo.ReadAll()
					on rental.CarId equals car.Id
					select new
					{
						Car = car.Id,
						Rental = rental.Id
					};
			var q2 = (from item in q
					 group item by item.Car into g
					 select new
					 {
						 Car = g.Key,
						 Count = q.Where(r => r.Car == g.Key).Count()
					 }).OrderByDescending(c => c.Count).FirstOrDefault();

			return carRepo.Read(q2.Car);
		}
	}
}
