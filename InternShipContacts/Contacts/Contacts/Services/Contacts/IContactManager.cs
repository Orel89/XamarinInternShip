using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Contacts
{
    public interface IContactManager
    {
        ContactModel Contact { get; set; }

        List<ContactModel> ContactList { get; set; }

        Task<List<ContactModel>> GetAllContactsAsync();

        Task<int> DeleteAsync(ContactModel profile);

        Task<ContactModel> GetContactById(int id);

        Task<int> CreateAsync(ContactModel profile);

        Task<int> UpdateAsync(ContactModel profile);
    }
}
