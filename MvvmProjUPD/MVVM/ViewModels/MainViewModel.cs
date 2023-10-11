using MvvmProjUPD.Core;
using MvvmProjUPD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmProjUPD.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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

        public RelayCommand NavigateToLoginPageCommand { get; set; }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToLoginPageCommand = new RelayCommand(o => { Navigation.NavigateTo<LoginViewModel>(); }, o => true);
        }
    }
}
