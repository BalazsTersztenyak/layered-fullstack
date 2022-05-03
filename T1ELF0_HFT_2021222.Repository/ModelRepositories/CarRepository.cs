using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Repository
{
	public class CarRepository : Repository<Car>, IRepository<Car>
	{
		public CarRepository(CarRentalDbContext ctx) : base(ctx) { }

		public override Car Read(int id)
		{
			return ctx.Cars.FirstOrDefault(c => c.Id == id);
		}

		public override void Update(Car item)
		{
			var old = Read(item.Id);
			foreach (var prop in old.GetType().GetProperties())
			{
				prop.SetValue(old, prop.GetValue(item));
			}
			ctx.SaveChanges();
		}
	}
}
