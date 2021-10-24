using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Contacts.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public string Login
        {
            get => Preferences.Get(nameof(Login), null);
            set => Preferences.Set(nameof(Login), value);
        }
    }
}
 