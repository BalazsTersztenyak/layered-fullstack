using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Logic;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class RentalController : ControllerBase
	{
		RentalLogic logic;

		public RentalController(RentalLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IQueryable<Rental> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public Rental Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] Rental value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] Rental value)
		{
			this.logic.Update(value);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			this.logic.Delete(id);
		}

		[HttpGet("RentCountByBrand")]
		public IEnumerable<CountByBrand> RentCountByBrand()
		{
			return this.logic.RentCountByBrand();
		}

		[HttpGet("RentedAfterMarch")]
		public IEnumerable<Car> RentedAfterMarch()
		{
			return this.logic.RentedAfterMarch();
		}

		[HttpGet("MostPopular")]
		public Car MostPopular()
		{
			return this.logic.MostPopular();
		}
	}
}
