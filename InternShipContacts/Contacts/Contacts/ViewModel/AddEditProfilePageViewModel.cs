using Acr.UserDialogs;
using Contacts.Model;
using Contacts.Services.Contacts;
using Contacts.Services.Settings;
using Contacts.View;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.ViewModel
{
    public class AddEditProfilePageViewModel : BindableBase, INavigationAware
    {
        ISettingsManager _settingManager;
        private readonly INavigationService _navigationService;
        IContactManager _contactManager;

        public AddEditProfilePageViewModel(INavigationService navigationService, IContactManager contactManager, ISettingsManager settingManager)
        {
            _navigationService = navigationService;
            _contactManager = contactManager;
            _settingManager = settingManager;
            _id = StaticHelpers.Helper.Id;
            Initialization(_id);
        }

        private int _id;

        #region --- Public Properties ---

        private ContactModel _contact;
        public ContactModel Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickname;
        public string NickName
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _pictureUrl = "user.png";
        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value);
        }
        private DateTime _creationtime;
        public DateTime CreationTime
        {
            get => _creationtime;
            set => SetProperty(ref _creationtime, value);
        }
      

        #endregion

        #region ---Commands---
        public ICommand OnSaveButton => new Command(Save);
        public ICommand OnArrowTapButton => new Command(ExecuteNavigateCommandToMainListPage);
        public ICommand OnTapImage => new Command(ActionDialog);
        #endregion


        #region ---private helpers---
        private async void Initialization(int id)
        {
            if (id >= 0)
            {
                Contact = await _contactManager.GetContactById(_id);
                Name = Contact.Name;
                CreationTime = Contact.CreationTime;
                NickName = Contact.NickName;
                Description = Contact.Description;
                PictureUrl = Contact.PictureUrl;
            }
            else if (id == -2)
            {
                Contact.Name = Name;
                Contact.NickName = NickName;
                Contact.CreationTime = DateTime.Now;
                Contact.Description = Description;
                Contact.PictureUrl = PictureUrl;
                Contact.UserLogin = _settingManager.Login;
            }
        }
        private async void Save()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(NickName)) return;

            Console.WriteLine("Name---" + Name);
            Console.WriteLine("Nickname---" + NickName);


            if (_id >= 0)
            {
                Contact = new ContactModel();
                Contact.Id = _id;
                Initialization(-2);
                await _contactManager.UpdateAsync(Contact);
            }
            else
            {
                Contact = new ContactModel();
                Initialization(-2);
                await _contactManager.CreateAsync(Contact);
            }
            ExecuteNavigateCommandToMainListPage();
        }
        private void ActionDialog()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                           .SetTitle("Choose Type")
                           .Add("Capture", PickImage, "woman.png")
                           .Add("File", PickFile, "woman.png")
                           .SetUseBottomSheet(false));
        }
        private async void PickImage()
        {
            {
                try
                {
                    var result = await MediaPicker.CapturePhotoAsync();
                    if (result != null)
                        PictureUrl = result.FullPath;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        private async void PickFile()
        {
            {
                try
                {
                    var result = await FilePicker.PickAsync(PickOptions.Default);
                    if (result != null)
                        PictureUrl = result.FullPath;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        #endregion

        #region ---Navigation---
        private async void ExecuteNavigateCommandToMainListPage()
        {
            await _navigationService.NavigateAsync(nameof(MainListPage));
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _id = parameters.GetValue<int>("id");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
