using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1ELF0_HFT_2021222.Logic;
using T1ELF0_HFT_2021222.Models;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Test
{
	[TestFixture]
	public class RentalTests
	{
		RentalLogic rl;
		Mock<IRepository<Rental>> mockRentalRepo;
		Mock<IRepository<Brand>> mockBrandRepo;
		Mock<IRepository<Car>> mockCarRepo;

		[SetUp]
		public void Init()
		{
			mockBrandRepo = new Mock<IRepository<Brand>>();
			mockCarRepo = new Mock<IRepository<Car>>();
			mockRentalRepo = new Mock<IRepository<Rental>>();
			rl = new RentalLogic(mockRentalRepo.Object, mockCarRepo.Object, mockBrandRepo.Object);

			mockRentalRepo.Setup(m => m.Read(3)).Returns(new Rental() { Id = 3 });
			mockRentalRepo.Setup(m => m.ReadAll()).Returns(new List<Rental>() 
			{ 
				new Rental() { Id = 1, CarId = 1, Date = "2020.10.10"},
				new Rental() { Id = 2, CarId = 3, Date = "2020.10.10"},
				new Rental() { Id = 3, CarId = 2, Date = "2020.10.10"},
				new Rental() { Id = 4, CarId = 2, Date = "2020.10.10"}
			} as IQueryable<Rental>);

			mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
			{
				new Car() { Id = 1, Age = 10, BrandId = 1, Model = "good", Price = 10000},
				new Car() { Id = 2, Age = 5, BrandId = 2, Model = "good", Price = 12000},
				new Car() { Id = 3, Age = 10, BrandId = 3, Model = "good", Price = 10500},
				new Car() { Id = 4, Age = 10, BrandId = 2, Model = "good", Price = 19000}
			} as IQueryable<Car>);

			mockRentalRepo.Setup(m => m.Delete(It.IsAny<int>())).Verifiable();
		}

		[Test]
		public void CreateRentalTest_WithCorrectID()
		{
			//Arrange
			var rental = new Rental() { Id = 5 };
			//Act
			rl.Create(rental);
			//Assert
			mockRentalRepo.Verify(r => r.Create(rental), Times.Once);
		}

		[Test]
		public void CreateRentalTest_WithInCorrectID()
		{
			//Arrange
			var rental = new Rental() { Id = 3 };
			//Act
			try
			{
				rl.Create(rental);
			}
			catch
			{

			}

			//Assert
			mockRentalRepo.Verify(r => r.Create(rental), Times.Never);
		}

		[Test]
		public void DeleteRentalTest()
		{
			rl.Delete(3);

			mockRentalRepo.Verify(r => r.Delete(3), Times.Once);
		}

		[Test]
		public void ReadRentalTest_WithCorrectID()
		{
			var rental = rl.Read(3);

			mockRentalRepo.Verify(r => r.Read(3), Times.Once);
		}
	}
}
