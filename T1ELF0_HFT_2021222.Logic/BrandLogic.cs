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

		public BrandLogic(IRepository<Brand> repo)
		{
			this.repo = repo;
		}

		public void Create(Brand item)
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

		public Brand Read(int id)
		{
			if (this.repo.Read(id) == null)
			{
				throw new Exception("Item not found");
			}

			return this.repo.Read(id);
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
	}
}
