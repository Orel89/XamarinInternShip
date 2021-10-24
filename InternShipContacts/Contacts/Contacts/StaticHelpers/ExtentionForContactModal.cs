using Contacts.Model;
using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.StaticHelpers
{
    public static class ExtentionForContactModal
    {
        public static ContactModel CreateContact(this ContactViewModel contact)
        {
            ContactModel contactModel = new ContactModel
            {
                Name = contact.Name,
                NickName = contact.NickName,
                UserLogin = contact.UserLogin,
                Id = contact.Id,
                Description = contact.Description,
                PictureUrl = contact.PictureUrl,
                CreationTime = contact.CreationTime
            };
            return contactModel;
        }
        public static ContactViewModel CreateContactViewModel(this ContactModel contact)
        {
            ContactViewModel contactViewModel = new ContactViewModel
            {
                Name = contact.Name,
                NickName = contact.NickName,
                UserLogin = contact.UserLogin,
                Id = contact.Id,
                Description = contact.Description,
                PictureUrl= contact.PictureUrl,
                CreationTime = contact.CreationTime
            };
            return contactViewModel;
        }
    }
}
