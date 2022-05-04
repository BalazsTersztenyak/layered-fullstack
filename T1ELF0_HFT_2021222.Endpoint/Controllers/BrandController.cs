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
	public class BrandController : ControllerBase
	{
		BrandLogic logic;

		public BrandController(BrandLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IQueryable<Brand> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public Brand Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] Brand value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] Brand value)
		{
			this.logic.Update(value);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			this.logic.Delete(id);
		}

		[HttpGet("/AVGByBrand")]
		public IEnumerable<BrandAVG> AVGByBrand()
		{
			return this.logic.AVGByBrand();
		}
	}
}
