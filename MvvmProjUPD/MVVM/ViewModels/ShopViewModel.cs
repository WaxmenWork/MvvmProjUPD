using MvvmProjUPD.Core;
using MvvmProjUPD.MVVM.Models;
using MvvmProjUPD.MVVM.Models.Repositories;
using MvvmProjUPD.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmProjUPD.MVVM.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {

        private INavigationService _navigation;
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private User _currentUserAccount;
        private string _finderText;
        private string _selectedProvider;
        private string _selectedManufacturer;
        private int page = 1;
        private int _selectedTake = 3;

        private ObservableCollection<string> _takes;
        private ObservableCollection<Product> products;
        private ObservableCollection<string> manufacturers;
        private ObservableCollection<string> providers;

        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public User CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged();
            }
        }

        public string FinderText
        {
            get => _finderText;
            set
            {
                _finderText = value;
                page = 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredPagedProducts));
            }
        }

        public string SelectedProvider
        {
            get => _selectedProvider;
            set
            {
                _selectedProvider = value;
                page = 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredPagedProducts));
            }
        }

        public string SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                _selectedManufacturer = value;
                page = 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredPagedProducts));
            }
        }

        public ObservableCollection<string> Manufacturers
        {
            get => manufacturers;
            set
            {
                manufacturers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Providers
        {
            get => providers;
            set
            {
                providers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Takes
        {
            get => _takes;
            set
            {
                _takes = value;
                OnPropertyChanged();
            }
        }

        public int SelectedTake
        {
            get => _selectedTake;
            set
            {
                _selectedTake = Convert.ToInt32(value);
                page = 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredPagedProducts));
            }
        }

        public int Page
        {
            get => page;
            set
            {
                page = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredPagedProducts));
            }
        }

        public RelayCommand NavigateToLoginPageCommand { get; set; }
        public RelayCommand NavigateToProfilePageCommand { get; set; }

        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand NextPageCommand { get; set; }
        public RelayCommand PrevPageCommand { get; set; }

        public IEnumerable<Product> FilteredProducts =>
            products.Where(a =>
                (string.IsNullOrWhiteSpace(FinderText) || a.ProductName.ToLower().Contains(FinderText.ToLower()) ||
                a.ProductArticleNumber.ToLower() == FinderText.ToLower() || a.ProductDescription.ToLower().Contains(FinderText.ToLower()) ||
                a.ProductCategory.ToLower().Contains(FinderText.ToLower())) &&
                (SelectedProvider == "Все поставщики" || a.ProductProvider == SelectedProvider) &&
                (SelectedManufacturer == "Все производители" || a.ProductManufacturer == SelectedManufacturer));

        public IEnumerable<Product> FilteredPagedProducts =>
            FilteredProducts.Skip((page - 1) * SelectedTake).Take(SelectedTake);

        public ShopViewModel(INavigationService navService, UserRepository userRepository, ProductRepository productRepository)
        {
            Navigation = navService;
            this.userRepository = userRepository;
            this.productRepository = productRepository;

            LoadCurrentUserData();

            Products = new ObservableCollection<Product>(productRepository.GetByAll());

            Takes = new ObservableCollection<string>();
            Takes.Add(Products.Count.ToString());
            for (int i = Products.Count / 2; i >= 3 ; i -= 3)
            {
                Takes.Add(i.ToString());
            }
            
            Providers = new ObservableCollection<string>(productRepository.GetAllProviders());
            Providers.Insert(0, "Все поставщики");
            SelectedProvider = Providers.FirstOrDefault();

            Manufacturers = new ObservableCollection<string>(productRepository.GetAllManufacturers());
            Manufacturers.Insert(0, "Все производители");
            SelectedManufacturer = Manufacturers.FirstOrDefault();

            NavigateToLoginPageCommand = new RelayCommand(o => { Navigation.NavigateTo<LoginViewModel>(); }, o => true);
            NavigateToProfilePageCommand = new RelayCommand(o => { Navigation.NavigateTo<ProfileViewModel>(); }, o => true);

            NextPageCommand = new RelayCommand(o => { Page += 1; }, o => (FilteredProducts.Count() - SelectedTake > (Page - 1) * SelectedTake));
            PrevPageCommand = new RelayCommand(o => { Page -= 1; }, o => (Page > 1));
            

            LogoutCommand = new RelayCommand(ExecuteLogoutCommand, o => true);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            Thread.CurrentPrincipal = null;
            Navigation.NavigateTo<LoginViewModel>();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByLogin(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount = user;
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
                NavigateToLoginPageCommand.Execute(null);
            }
        }
    }
}
