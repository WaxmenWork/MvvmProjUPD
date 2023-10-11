using MvvmProjUPD.Core;
using MvvmProjUPD.MVVM.Models.Repositories;
using MvvmProjUPD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Principal;

namespace MvvmProjUPD.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private SecureString _password;
        private string _login;
        private string _errorMessage;
        private bool _isValidUser = false;

        private UserRepository userRepository;

        private INavigationService _navigation;
        
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidUser
        {
            get => _isValidUser;
            set
            {
                _isValidUser = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; }
        public RelayCommand ShowPasswordCommand { get; }
        public RelayCommand RememberPasswordCommand { get;}

        public RelayCommand NavigateToShopPageCommand { get; set; }

        public LoginViewModel(INavigationService navigation, UserRepository userRepository)
        {
            Navigation = navigation;
            NavigateToShopPageCommand = new RelayCommand(o => { Navigation.NavigateTo<ShopViewModel>(); }, o => true);
            this.userRepository = userRepository;
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Login) || Login.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            bool isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Login, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Login), null);
                IsValidUser = true;
            } else
            {
                ErrorMessage = "*Неверный логин или пароль";
            }
        }
    }
}
