using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using T1ELF0_HFT_2021222.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
		private string errorMessage;

		public string ErrorMessage
		{
			get { return errorMessage; }
			set { SetProperty(ref errorMessage, value); }
		}


		public RestCollection<Brand> Brands { get; set; }

		private Brand selectedBrand;

		public Brand SelectedBrand
		{
			get { return selectedBrand; }
			set
			{
				if (value != null)
				{
					selectedBrand = new Brand()
					{
						Name = value.Name,
						Id = value.Id
					};
					OnPropertyChanged();
					(DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
				}
			}
		}

		public RestCollection<Car> Cars { get; set; }

		private Car selectedCar;

		public Car SelectedCar
		{
			get { return selectedCar; }
			set
			{
				if (value != null)
				{
					selectedCar = new Car()
					{
						Brand = value.Brand,
						Model = value.Model,
						Age = value.Age,
						Price = value.Price,
						Id = value.Id
					};
					OnPropertyChanged();
					(DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
				}
			}
		}

		public RestCollection<Rental> Rentals { get; set; }

		private Rental selectedRental;

		public Rental SelectedRental
		{
			get { return selectedRental; }
			set
			{
				if (value != null)
				{
					selectedRental = new Rental()
					{
						CarId = value.CarId,
						Date = value.Date,
						Id = value.Id
					};
					OnPropertyChanged();
					(DeleteRentalCommand as RelayCommand).NotifyCanExecuteChanged();
				}
			}
		}

		private string noncrud;

		public string Noncrud
		{
			get { return noncrud; }
			set
			{
				if (value != null)
				{
					noncrud = value;
					OnPropertyChanged();
				}
			}
		}

		private IEnumerable<IEntity> noncrudElements;

		public IEnumerable<IEntity> NoncrudElements
		{
			get { return noncrudElements; }
			set
			{
				if (value != null)
				{
					noncrudElements = value;
					OnPropertyChanged();
				}
			}
		}
		

		public ICommand CreateBrandCommand { get; set; }

		public ICommand DeleteBrandCommand { get; set; }

		public ICommand UpdateBrandCommand { get; set; }


		public ICommand CreateCarCommand { get; set; }

		public ICommand DeleteCarCommand { get; set; }

		public ICommand UpdateCarCommand { get; set; }


		public ICommand CreateRentalCommand { get; set; }

		public ICommand DeleteRentalCommand { get; set; }

		public ICommand UpdateRentalCommand { get; set; }


		public ICommand AVGByBrandCommand { get; set; }

		public ICommand CountByBrandCommand { get; set; }

		public ICommand RentCountByBrandCommand { get; set; }

		public ICommand RentedAfterMarchCommand { get; set; }

		public ICommand MostPopularCommand { get; set; }

		public static bool IsInDesignMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		RestService restCar;
		RestService restBrand;
		RestService restRental;

		public MainWindowViewModel()
		{
			if (!IsInDesignMode)
			{
				Brands = new RestCollection<Brand>("http://localhost:42910/", "brand");
				CreateBrandCommand = new RelayCommand(() =>
				{
					Brands.Add(new Brand()
					{
						Name = SelectedBrand.Name
					});
				});

				UpdateBrandCommand = new RelayCommand(() =>
				{
					try
					{
						Brands.Update(SelectedBrand);
					}
					catch (ArgumentException ex)
					{
						ErrorMessage = ex.Message;
					}

				});

				DeleteBrandCommand = new RelayCommand(() =>
				{
					Brands.Delete(SelectedBrand.Id);
				},
				() =>
				{
					return SelectedBrand != null;
				});
				SelectedBrand = new Brand();


				Cars = new RestCollection<Car>("http://localhost:42910/", "car");
				CreateCarCommand = new RelayCommand(() =>
				{
					Cars.Add(new Car()
					{
						Id = SelectedCar.Id,
						BrandId = SelectedCar.Id,
						Age = SelectedCar.Age,
						Model = SelectedCar.Model,
						Brand = SelectedCar.Brand,
						Price = SelectedCar.Price
					});
				});

				UpdateCarCommand = new RelayCommand(() =>
				{
					try
					{
						Cars.Update(SelectedCar);
					}
					catch (ArgumentException ex)
					{
						ErrorMessage = ex.Message;
					}

				});

				DeleteCarCommand = new RelayCommand(() =>
				{
					Cars.Delete(SelectedCar.Id);
				},
				() =>
				{
					return SelectedCar != null;
				});
				SelectedCar = new Car();


				Rentals = new RestCollection<Rental>("http://localhost:42910/", "rental");
				CreateRentalCommand = new RelayCommand(() =>
				{
					Rentals.Add(new Rental()
					{
						Id = SelectedRental.Id,
						CarId = SelectedRental.Id,
						Date = SelectedRental.Date
					});
				});

				UpdateRentalCommand = new RelayCommand(() =>
				{
					try
					{
						Rentals.Update(SelectedRental);
					}
					catch (ArgumentException ex)
					{
						ErrorMessage = ex.Message;
					}

				});

				DeleteRentalCommand = new RelayCommand(() =>
				{
					Rentals.Delete(SelectedRental.Id);
				},
				() =>
				{
					return SelectedRental != null;
				});
				SelectedRental = new Rental();


				restCar = new RestService("http://localhost:42910/", "car");
				restBrand = new RestService("http://localhost:42910/", "brand");
				restRental = new RestService("http://localhost:42910/", "rental");

				MostPopularCommand = new RelayCommand(() =>
				{
					MostPopular();
				});

				AVGByBrandCommand = new RelayCommand(() =>
				{
					AVGByBrand();
				});

				RentedAfterMarchCommand = new RelayCommand(() =>
				{
					RentedAfterMarch();
				});

				CountByBrandCommand = new RelayCommand(() =>
				{
					CountByBrand();
				});

				RentCountByBrandCommand = new RelayCommand(() =>
				{
					RentCountByBrand();
				});
			}

		}

		private void MostPopular()
		{
			Car car = restRental.Get<Car>("MostPopular", "rental");
			List<Car> cars = new List<Car>();
			cars.Add(car);
			NoncrudElements = cars;
			Noncrud = "Most Popular";
		}

		private void RentedAfterMarch()
		{
			var cars = restRental.Get<IEnumerable<Car>>("RentedAfterMarch", "rental");
			NoncrudElements = cars;
			Noncrud = "Rented After March";
		}

		private void AVGByBrand()
		{
			var avgs = restBrand.Get<IEnumerable<BrandAVG>>("AVGByBrand", "brand");
			NoncrudElements = avgs;
			Noncrud = "AVG by Brand";
		}

		internal class BrandAVG : IEntity
		{
			public string Name { get; set; }
			public double AVG { get; set; }
			public int Id { get { return (int)Math.Round(AVG); } set => throw new NotImplementedException(); }
		}

		private void CountByBrand()
		{
			var counts = restCar.Get<IEnumerable<BrandCount>>("CountByBrand", "car");
			NoncrudElements = counts;
			Noncrud = "Count by Brand";
		}

		internal class BrandCount : IEntity
		{
			public string Name { get; set; }
			public int Id { get; set; }
		}

		private void RentCountByBrand()
		{
			var counts = restRental.Get<IEnumerable<BrandCount>>("RentCountByBrand", "rental");
			NoncrudElements = counts;
			Noncrud = "Rent Count by Brand";
		}
	}
}
