using Contacts.Model;
using Contacts.Services.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Contacts.Services.AuthenticationService;
using Acr.UserDialogs;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Linq;
using Contacts.View;

namespace Contacts.ViewModel
{
    public class SignUpPageViewModel : BindableBase
    {
        private string _login;
        private string _password;
        private string _confirmPassword;
        private readonly INavigationService _navigationService;
        private IAuthentication _authenticationService;


        public SignUpPageViewModel(INavigationService navigationService, IAuthentication authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
        }
        public ICommand NavigateCommandToMainPage => new Command(ValidationAsync);

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
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

  
        

        #region ---Privat helpers---
        private async void ValidationAsync()
        {
            var confirmConfig = new ConfirmConfig()
            {
                OkText = "Ok",
                CancelText = ""
            };
            if (_password == null || _login == null)
            {
                confirmConfig.Message = "Provide Password and login";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            if (_confirmPassword != _password)
            {
                confirmConfig.Message = "The passwords don't match";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }

            var m = new Regex(@"^\d{1}").Matches(_login);
            if (m.Count > 0)
            {
                confirmConfig.Message = "login must not start with a number";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            else
            if (_login.Length < 4 || _login.Length > 16)
            {
                confirmConfig.Message = "login must be at least 4 characters and no more than 16 characters";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }

            if (_password.Length < 8 || _password.Length > 16)
            {
                confirmConfig.Message = "Password must be at least 8 characters and no more than 16 characters";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            else
            if (!_password.Any(char.IsLetter))
            {
                confirmConfig.Message = "Password must contain letters";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            else
            if (!_password.Any(char.IsDigit))
            {
                confirmConfig.Message = "Password must contain numbers";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            else
            if (!_password.Any(char.IsLower))
            {
                confirmConfig.Message = "Password must contain lowercase";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            else
            if (!_password.Any(char.IsUpper))
            {
                confirmConfig.Message = "Password must contain uppercase";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            if (!await _authenticationService.RegistrationAsync(Login, Password))
            {
                confirmConfig.Message = "This login is already taken";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            StaticHelpers.Helper.Login = Login;
            ExecuteNavigateCommandToMainPage();

        }


        #endregion

        #region---Navigation---
        async void ExecuteNavigateCommandToMainPage()
        {
            await _navigationService.NavigateAsync(nameof(MainPage));
        }
        #endregion

    }
}
