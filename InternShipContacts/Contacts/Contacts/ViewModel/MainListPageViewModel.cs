using Acr.UserDialogs;
using Contacts.Model;
using Contacts.Services.AuthenticationService;
using Contacts.Services.Contacts;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Contacts.StaticHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.View;

namespace Contacts.ViewModel
{
    public class MainListPageViewModel : BindableBase
    {
        private IContactManager _contactManager;
        private IAuthentication _authenticationService;
        private readonly INavigationService _navigationService;
        #region ---Public Properties---

        public MainListPageViewModel(INavigationService navigationService, IAuthentication authenticationService, IContactManager contactManager)
        {
            _contactManager = contactManager;
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            InitializationObservableCollection();
        }

        private ObservableCollection<ContactViewModel> listOfConactst;
        public ObservableCollection<ContactViewModel> ContactList 
        {
            get => listOfConactst;
            set => SetProperty(ref listOfConactst, value);
        }
        #endregion
        #region---Commands---
        public ICommand OnAddButtonTap => new Command(AddNewContact);
        public ICommand OnSettingsButtonTap => new Command(NavigateToSettingsPage);
        public ICommand OnLogOutButtonTap => new Command(ClouseAuthorisation);
        #endregion



        #region ---Navigation---
        private async void NavigateToSettingPageMethod()
        {
            await _navigationService.NavigateAsync(nameof(SettingPage));
        }
        
        private async void NavigateToMainPage()
        {
            await _navigationService.NavigateAsync(nameof(MainPage));
        }

        #endregion


        #region ---Private Helpers---
        private async void InitializationObservableCollection()
        {
            var contactsList = await _contactManager.GetAllContactsAsync();
            var collectionOfContactViewModel = new ObservableCollection<ContactViewModel>();
            var contactViewModels = contactsList.Select(c => c.CreateContactViewModel());
            foreach (var contact in contactViewModels)
            {
                if (contact.PictureUrl == null) contact.PictureUrl = "user.png";
                contact.DeleteCommand = new Command(DeleteAsync);
                contact.EditCommand = new Command(GoEdit);
                collectionOfContactViewModel.Add(contact);
            }
            ContactList = collectionOfContactViewModel;
        }
        private void AddNewContact(object obj)
        {
            StaticHelpers.Helper.Id = -1;
            NavigateToAddEditProfilePage(-1); 
        }
       

        private void GoEdit(object contactObject)
        {
            StaticHelpers.Helper.Id = (contactObject as ContactViewModel).Id;
            NavigateToAddEditProfilePage((contactObject as ContactViewModel).Id);
        }
        private void ClouseAuthorisation(object obj)
        {
            _authenticationService.CloseAutorization();

            NavigateToMainPage();
        }
        private void NavigateToSettingsPage(object obj)
        {
            NavigateToSettingPageMethod();
        }

        private async void NavigateToAddEditProfilePage(int id)
        {
            var parameter = new NavigationParameters();
            parameter.Add("id", id);
            await _navigationService.NavigateAsync(nameof(AddEditProfilePage), parameter);
        }

        private async void DeleteAsync(object contactObject)
        {
            if (contactObject != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "Do you want to delete this contact?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);

                if (confirm)
                {
                    ContactList.Remove(contactObject as ContactViewModel);
                    await _contactManager.DeleteAsync((contactObject as ContactViewModel).CreateContact());
                }
            }
        }

        #endregion
    }
}
