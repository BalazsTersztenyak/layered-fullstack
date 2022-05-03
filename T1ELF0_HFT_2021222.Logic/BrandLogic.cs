using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Models;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Logic
{
	public class BrandLogic
	{
		IRepository<Brand> repo;
		IRepository<Car> carRepo;

		public BrandLogic(IRepository<Brand> repo, IRepository<Car> carRepo)
		{
			this.repo = repo;
			this.carRepo = carRepo;
		}

		public void Create(Brand item)
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

		public IEnumerable<Brand> Read(int id)
		{
			if (this.repo.Read(id) == null)
			{
				throw new Exception("Item not found");
			}

			return this.repo.Read(id) as IEnumerable<Brand>;
		}

		public IQueryable<Brand> ReadAll()
		{
			return this.repo.ReadAll();
		}

		public void Update(Brand item)
		{
			if (Read(item.Id) == null)
			{
				throw new Exception("Item not found");
			}

			this.repo.Update(item);
		}

		public IEnumerable<BrandAVG> AVGByBrand()
		{
			var q = from brand in repo.ReadAll()
					join car in carRepo.ReadAll()
					on brand.Id equals car.BrandId
					select new
					{
						Brand = brand.Name,
						Price = car.Price
					};
			var q2 = from item in q
					 group item by item.Brand into g
					 select new BrandAVG()
					 {
						 Name = g.Key,
						 AVG = q.Where(c => c.Brand == g.Key).Select(c => c.Price).Average()
					 };

			return q2;
		}
	}

	public class BrandAVG
	{
		public string Name { get; set; }
		public double AVG { get; set; }
	}
}
