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
	public class CarController : ControllerBase
	{
		CarLogic logic;

		public CarController(CarLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IQueryable<Car> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public Car Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] Car value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] Car value)
		{
			this.logic.Update(value);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			this.logic.Delete(id);
		}

		[HttpGet("CountByBrand")]
		public IEnumerable<BrandCount> CountByBrand()
		{
			return this.logic.CountByBrand();
		}
	}
}
