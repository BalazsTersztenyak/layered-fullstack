using Moq;
using NUnit.Framework;
using System;
using T1ELF0_HFT_2021222.Logic;
using T1ELF0_HFT_2021222.Models;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Test
{
	[TestFixture]
	public class BrandTests
	{
		BrandLogic bl;
		Mock<IRepository<Brand>> mockBrandRepo;
		Mock<IRepository<Car>> mockCarRepo;

		[SetUp]
		public void Init()
		{
			mockBrandRepo = new Mock<IRepository<Brand>>();
			mockCarRepo = new Mock<IRepository<Car>>();
			bl = new BrandLogic(mockBrandRepo.Object, mockCarRepo.Object);

			mockBrandRepo.Setup(m => m.Read(3)).Returns(new Brand() { Id = 3, Name = "brand"});

			mockBrandRepo.Setup(m => m.Delete(It.IsAny<int>())).Verifiable();
		}

		[Test]
		public void CreateBrandTest_WithCorrectID()
		{
			//Arrange
			var brand = new Brand() { Id = 5 };
			//Act
			bl.Create(brand);
			//Assert
			mockBrandRepo.Verify(r => r.Create(brand), Times.Once);
		}

		[Test]
		public void CreateBrandTest_WithInCorrectID()
		{
			//Arrange
			var brand = new Brand() { Id = 3 };
			//Act
			try
			{
				bl.Create(brand);
			}
			catch
			{

			}
			
			//Assert
			mockBrandRepo.Verify(r => r.Create(brand), Times.Never);
		}

		[Test]
		public void DeleteBrandTest()
		{
			bl.Delete(3);

			mockBrandRepo.Verify(r => r.Delete(3), Times.Once);
		}
	}
}
