using System;
using System.Linq;
using T1ELF0_HFT_2021222.Models;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Logic
{
	public class CarLogic
	{
		IRepository<Car> repo;

		public CarLogic(IRepository<Car> repo)
		{
			this.repo = repo;
		}

		public void Create(Car item)
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

		public Car Read(int id)
		{
			if (this.repo.Read(id) == null)
			{
				throw new Exception("Item not found");
			}

			return this.repo.Read(id);
		}

		public IQueryable<Car> ReadAll()
		{
			return this.repo.ReadAll();
		}

		public void Update(Car item)
		{
			if (Read(item.Id) == null)
			{
				throw new Exception("Item not found");
			}

			this.repo.Update(item);
		}
	}
}
