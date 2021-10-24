using Contacts.Model;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace Contacts.Services.AuthenticationService
{
    public class AuthenticationService : IAuthentication
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepository _repository;
        public AuthenticationService(IRepository repository, ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }

        public async Task<bool> RegistrationAsync(string login, string password)
        {
            bool isDone = true;

            var list = await _repository.GetAllAsync<UserModel>();

            foreach (var userModel in list)
            {
                if (userModel.Login == login)
                {
                    isDone = false;
                }
            }
            if (isDone)
            {
                UserModel user = new UserModel()
                {
                    Login = login,
                    Password = password,
                };
                await _repository.InsertAsync(user);
            }
            return await Task.Run(() => isDone);

        }

        public async Task<string> AutorizationAsync(string login, string password)
        {
            string success = "Lack of login!";

            var list = await _repository.GetAllAsync<UserModel>();

            foreach (var userModel in list)
            {
                if (userModel.Login == login)
                {
                    success = "Lack of Password!";

                    if (userModel.Password == password)
                    {
                        success = "success";

                        _settingsManager.Login = login; 
                    }
                }
            }

            return await Task.Run(() => success);
        }

        public void CloseAutorization()
        {
            _settingsManager.Login = null;
        }
    }
}
