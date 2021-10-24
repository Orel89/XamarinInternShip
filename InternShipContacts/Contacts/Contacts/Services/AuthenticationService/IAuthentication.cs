using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.AuthenticationService
{
    public interface IAuthentication
    {
        void CloseAutorization();


        Task<string> AutorizationAsync(string login, string password);


        Task<bool> RegistrationAsync(string login, string password);
    }
}
