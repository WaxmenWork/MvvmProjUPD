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
    public class ProfileViewModel : ViewModelBase
    {
        private UserRepository userRepository;
        private User _currentUser;
        private ObservableCollection<Order> _userOrders;
        private INavigationService _navigation;
        private string userDisplayName;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Order> UserOrders
        {
            get => _userOrders; 
            set
            {
                _userOrders = value;
                OnPropertyChanged();
            }
        }

        public string UserDisplayName
        {
            get => userDisplayName;
            set
            {
                userDisplayName = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand NavigateToLoginPageCommand { get; set; }
        public RelayCommand NavigateToShopPageCommand { get; set; }


        public ProfileViewModel(INavigationService navService, UserRepository userRepository)
        {
            Navigation = navService;
            this.userRepository = userRepository;
            LoadCurrentUserData();
            UserDisplayName = $"{CurrentUser.UserSurname} {CurrentUser.UserName} {CurrentUser.UserPatronymic}";
            NavigateToLoginPageCommand = new RelayCommand(o => { Navigation.NavigateTo<LoginViewModel>(); }, o => true);
            NavigateToShopPageCommand = new RelayCommand(o => { Navigation.NavigateTo<ShopViewModel>(); }, o => true);
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByLogin(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUser = user;
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
                NavigateToLoginPageCommand.Execute(null);
            }
        }
    }
}
