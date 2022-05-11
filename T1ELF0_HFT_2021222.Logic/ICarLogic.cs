using System.Collections.Generic;
using System.Linq;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Logic
{
	public interface ICarLogic
	{
		IEnumerable<BrandCount> CountByBrand();
		void Create(Car item);
		void Delete(int id);
		Car Read(int id);
		IQueryable<Car> ReadAll();
		void Update(Car item);
	}
}