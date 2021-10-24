using Contacts.Model;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Contacts
{
    public class ContactManager: IContactManager
    {

        private ISettingsManager _settingsManager;
        private IRepository _repository;

        public ContactManager(ISettingsManager settingsManager, IRepository repository)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }



        #region --- Public Properties ---

        public ContactModel Contact {get;set;}

        public List<ContactModel> ContactList {get; set;}
        private string _name;
        public string Name
        {
            get => _settingsManager.Login;
            set { _settingsManager.Login = value; }
        }

        public async Task<ContactModel> GetContactById(int id)
        {
            ContactList = await GetAllContactsAsync();
            ContactModel contactModel = new ContactModel();
            foreach (ContactModel model in ContactList)
            {
                if (model.Id == id) contactModel = model;
            }
            return contactModel;
        }
        public async Task<int> UpdateAsync(ContactModel contact)
        {
            return await _repository.UpdateAsync(contact);
        }
        public async Task<int> DeleteAsync(ContactModel contact)
        {
            return await _repository.DeleteAsync(contact);
        }

        public async Task<int> CreateAsync(ContactModel contact)
        {
            return await _repository.InsertAsync<ContactModel>(contact);
        }
        
       

        public async Task<List<ContactModel>> GetAllContactsAsync()
        {
            List<ContactModel> contactList = await _repository.GetAllAsync<ContactModel>();

            ContactList = new List<ContactModel>();

            var segregetedList = contactList.Where(c => c.UserLogin == _settingsManager.Login);

            foreach (ContactModel model in segregetedList)
            {
                ContactList.Add(model);
            }

            return ContactList;

        }
        #endregion
    }
}
