using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Repository
{
	public class RentalRepository : Repository<Rental>, IRepository<Rental>
	{
		public RentalRepository(CarRentalDbContext ctx) : base(ctx) { }

		public override Rental Read(int id)
		{
			return ctx.Rentals.FirstOrDefault(b => b.Id == id);
		}

		public override void Update(Rental item)
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
