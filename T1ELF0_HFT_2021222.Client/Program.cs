using System;
using T1ELF0_HFT_2021222.Repository;

namespace T1ELF0_HFT_2021222.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			CarRentalDbContext ctx = new CarRentalDbContext();
		}
	}
}
