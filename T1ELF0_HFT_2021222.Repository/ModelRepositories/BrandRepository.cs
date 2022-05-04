using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Repository
{
	public class BrandRepository : Repository<Brand>, IRepository<Brand>
	{
		public BrandRepository(CarRentalDbContext ctx) : base(ctx) { }

		public override Brand Read(int id)
		{
			return ctx.Brands.FirstOrDefault(b => b.Id == id);
		}

		public override void Update(Brand item)
		{
			var old = Read(item.Id) as Brand;
			foreach (var prop in item.GetType().GetProperties())
			{
				prop.SetValue(old, prop.GetValue(item));
			}
			ctx.SaveChanges();
		}
	}
}
