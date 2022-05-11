using System.Collections.Generic;
using System.Linq;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Logic
{
	public interface IBrandLogic
	{
		IEnumerable<BrandAVG> AVGByBrand();
		void Create(Brand item);
		void Delete(int id);
		Brand Read(int id);
		IQueryable<Brand> ReadAll();
		void Update(Brand item);
	}
}