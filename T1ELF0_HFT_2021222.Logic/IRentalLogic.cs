using System.Collections.Generic;
using System.Linq;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Logic
{
	public interface IRentalLogic
	{
		void Create(Rental item);
		void Delete(int id);
		Car MostPopular();
		Rental Read(int id);
		IQueryable<Rental> ReadAll();
		IEnumerable<CountByBrand> RentCountByBrand();
		IEnumerable<Car> RentedAfterMarch();
		void Update(Rental item);
	}
}