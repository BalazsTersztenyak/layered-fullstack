using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T1ELF0_HFT_2021222.Models
{
	public class Car
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey(nameof(Brand))]
		public int BrandId { get; set; }

		[NotMapped]
		public virtual Brand Brand { get; set; }

		[StringLength(20)]
		public string Model { get; set; }

		[Range(0, 30)]
		public int Age { get; set; }

		[Range(100, 500)]
		public int Price { get; set; }
	}
}
