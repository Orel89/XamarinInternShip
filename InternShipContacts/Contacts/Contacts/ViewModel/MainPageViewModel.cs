using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Contacts.ViewModel
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _navigateCommandForSingUpPage;
        private DelegateCommand _navigateCommandForMainListPage;

        public MainPageViewModel() {}
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
        #region ---Navigation---
        public DelegateCommand NavigateCommandToSingUpPage => 
            _navigateCommandForSingUpPage ?? (_navigateCommandForSingUpPage = new DelegateCommand(ExecuteNavigateCommandToSingUpPage));


        public DelegateCommand NavigateCommandToMailListPage =>
            _navigateCommandForMainListPage ?? (_navigateCommandForMainListPage = new DelegateCommand(ExecuteNavigateCommandToMainListPage, ()=> false));

        async void ExecuteNavigateCommandToSingUpPage()
        {
            await _navigationService.NavigateAsync("SignUpPage");
        }
        async void ExecuteNavigateCommandToMainListPage()
        {
            await _navigationService.NavigateAsync("MainListPage");
        }
        #endregion

    
    }
}
