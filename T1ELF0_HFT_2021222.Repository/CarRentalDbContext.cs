using Microsoft.EntityFrameworkCore;
using System;
using T1ELF0_HFT_2021222.Models;

namespace T1ELF0_HFT_2021222.Repository
{
	public class CarRentalDbContext : DbContext
	{
		public DbSet<Car> Cars { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Rental> Rentals { get; set; }

		public CarRentalDbContext()
		{
			this.Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("rental")
					.UseLazyLoadingProxies();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Car>(car => car
					.HasOne<Brand>()
					.WithMany()
					.HasForeignKey(car => car.BrandId)
					.OnDelete(DeleteBehavior.Cascade));

			modelBuilder.Entity<Rental>(rental => rental
					.HasOne<Car>()
					.WithMany()
					.HasForeignKey(rental => rental.CarId)
					.OnDelete(DeleteBehavior.Cascade));

			Brand bmw = new Brand() { Id = 1, Name = "BMW" };
			Brand citroen = new Brand() { Id = 2, Name = "Citroen" };
			Brand audi = new Brand() { Id = 3, Name = "Audi" };

			Car bmw1 = new Car() { Id = 1, BrandId = bmw.Id, Price = 20000, Model = "BMW 116d" };
			Car bmw2 = new Car() { Id = 2, BrandId = bmw.Id, Price = 30000, Model = "BMW 510" };
			Car citroen1 = new Car() { Id = 3, BrandId = citroen.Id, Price = 10000, Model = "Citroen C1" };
			Car citroen2 = new Car() { Id = 4, BrandId = citroen.Id, Price = 15000, Model = "Citroen C3" };
			Car audi1 = new Car() { Id = 5, BrandId = audi.Id, Price = 20000, Model = "Audi A3" };
			Car audi2 = new Car() { Id = 6, BrandId = audi.Id, Price = 25000, Model = "Audi A4" };

			Rental rent1 = new Rental() { Id = 1, CarId = 1, Date = "2022.03.03" };
			Rental rent2 = new Rental() { Id = 2, CarId = 2, Date = "2022.04.03" };
			Rental rent3 = new Rental() { Id = 3, CarId = 1, Date = "2022.04.04" };
			Rental rent4 = new Rental() { Id = 4, CarId = 1, Date = "2022.03.03" };
			Rental rent5 = new Rental() { Id = 5, CarId = 1, Date = "2022.03.03" };

			modelBuilder.Entity<Brand>().HasData(new Brand[]
			{
				bmw, citroen, audi
			});

			modelBuilder.Entity<Car>().HasData(new Car[]
			{
				bmw1, bmw2, citroen1, citroen2, audi1, audi2
			});

			modelBuilder.Entity<Rental>().HasData(new Rental[]
			{
				rent1, rent2, rent3, rent4, rent5
			});
		}
	}
}
