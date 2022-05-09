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
	public class CarTests
	{
		CarLogic cl;
		Mock<IRepository<Brand>> mockBrandRepo;
		Mock<IRepository<Car>> mockCarRepo;

		[SetUp]
		public void Init()
		{
			mockBrandRepo = new Mock<IRepository<Brand>>();
			mockCarRepo = new Mock<IRepository<Car>>();
			cl = new CarLogic(mockCarRepo.Object, mockBrandRepo.Object);

			mockCarRepo.Setup(m => m.Read(3)).Returns(new Car() { Id = 3 });

			mockCarRepo.Setup(m => m.Delete(It.IsAny<int>())).Verifiable();
		}

		[Test]
		public void CreateCarTest_WithCorrectID()
		{
			//Arrange
			var car = new Car() { Id = 5 };
			//Act
			cl.Create(car);
			//Assert
			mockCarRepo.Verify(r => r.Create(car), Times.Once);
		}

		[Test]
		public void CreateCarTest_WithInCorrectID()
		{
			//Arrange
			var car = new Car() { Id = 3 };
			//Act
			try
			{
				cl.Create(car);
			}
			catch
			{

			}

			//Assert
			mockCarRepo.Verify(r => r.Create(car), Times.Never);
		}

		[Test]
		public void DeleteCarTest()
		{
			cl.Delete(3);

			mockCarRepo.Verify(r => r.Delete(3), Times.Once);
		}
	}
}
