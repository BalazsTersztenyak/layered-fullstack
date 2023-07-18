using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace T1ELF0_HFT_2021222.Models
{
	public class Car :IEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey(nameof(Brand))]
		public int BrandId { get; set; }

		[NotMapped]
		[JsonIgnore]
		public virtual Brand Brand { get; set; }

		public string Model { get; set; }

		public int Age { get; set; }

		public int Price { get; set; }
	}
}
