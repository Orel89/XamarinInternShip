using Contacts.Model;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModel
{
    public class MainListPageViewModel : BindableBase, IInitializeAsync 
    {
        private ISettingsManager _settingsManager;
        private IRepository _repository;

        #region ---Public Properties---

        public MainListPageViewModel(ISettingsManager settingsManager, IRepository repositiry)
        {
          // _settingsManager = settingsManager;
            _repository = repositiry;
          
        }

        public ICommand AddButtonTapCommand => new Command(AddButtonTap);


        private string _firstName;
        private string _lastName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        private ObservableCollection<ProfileModel> _profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }


        #endregion 


        #region ---public Methods---

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var profileList =  await _repository.GetAllAsync<ProfileModel>();
            ProfileList = new ObservableCollection<ProfileModel>(profileList);
        }
        #endregion
        #region ---Ovverides---


        #endregion

        #region ---Private Helpers---

        private async void AddButtonTap(object obj)
        {
            var profile = new ProfileModel()
            {
                FirstName = _firstName,
                LastName = _lastName,
                CreationTime = DateTime.Now
            };
           var id = await _repository.InsertAsync(profile);
            profile.Id = id;
            ProfileList.Add(profile);
        }

        
        #endregion
    }
}
