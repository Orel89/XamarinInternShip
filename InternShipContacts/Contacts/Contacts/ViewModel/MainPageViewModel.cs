using Acr.UserDialogs;
using Contacts.Services.AuthenticationService;
using Contacts.Services.Settings;
using Contacts.View;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace Contacts.ViewModel
{
    public class MainPageViewModel : BindableBase
    {
        private string _login;
        private string _password;
        private IAuthentication _authenticationService;
        private readonly INavigationService _navigationService;
        private ISettingsManager _settingsManager;

        public MainPageViewModel(INavigationService navigationService, IAuthentication authenticationService, ISettingsManager settingsManager) 
        {
            _login = StaticHelpers.Helper.Login;
            _authenticationService = authenticationService;
            _settingsManager = settingsManager;
            _navigationService = navigationService;


        }
        #region ---Public Properties---
        public ICommand NavigateCommandToMailListPage => new Command(AuthorizatioMethod);
        public ICommand NavigateCommandToSingUpPage => new Command(ExecuteNavigateCommandToSingUpPage);

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

       
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion
        

        #region ---Private Method---
        private async void AuthorizatioMethod(object obj)
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password)) return;

            string result = await _authenticationService.AutorizationAsync(_login, _password);
            var confirmConfig = new ConfirmConfig()
            {
                OkText = "Ok",
                CancelText = ""
            };
            if (result == "success")
            {
                StaticHelpers.Helper.Login = Login;
                await _navigationService.NavigateAsync(nameof(MainListPage));
            }
            else
            if (result == "Lack of Password!")
            {
                confirmConfig.Message = "Incorrect password!";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                Password = "";
                return;
            }
            else
            if (result == "Lack of login!")
            {
                confirmConfig.Message = "The login not found!";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                Login = "";
                return;
            }
         
        }
        #endregion

        #region ---Navigation---
        async void ExecuteNavigateCommandToSingUpPage()
        {
            await _navigationService.NavigateAsync(nameof(SignUpPage));
        }
        #endregion


    }
}
