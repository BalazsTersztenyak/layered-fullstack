﻿using System;
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
			if (Read(item.Id) != null)
			{
				throw new Exception("Id already in use");
			}

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

		public IEnumerable<Rental> Read(int id)
		{
			if (this.repo.Read(id) == null)
			{
				throw new Exception("Item not found");
			}

			return this.repo.Read(id) as IEnumerable<Rental>;
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

		public IEnumerable<CountByBrand> RentCountByBrand()
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
					 select new CountByBrand()
					 {
						 Name = g.Key,
						 Count = q.Where(r => r.Brand == g.Key).Count()
					 };

			return q2;
		}

		public IEnumerable<Car> RentedAfterMarch()
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

		public IEnumerable<Car> MostPopular()
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

			return carRepo.Read(q2.Car) as IEnumerable<Car>;
		}
	}

	public class CountByBrand
	{
		public int Count { get; set; }
		public string Name { get; set; }
	}
}
