using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    public class ContactViewModel : BindableBase
    {
        #region ---Public Properties---

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);

        }

        private string _userLogin;
        public string UserLogin
        {
            get => _userLogin;
            set => SetProperty(ref _userLogin, value);
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

        private string _pictureUrl;
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
        private ICommand _editcommand;
        public ICommand EditCommand
        {
            get => _editcommand;
            set => SetProperty(ref _editcommand, value);

        }
        private ICommand _deletecommand;
        public ICommand DeleteCommand
        {
            get => _deletecommand;
            set => SetProperty(ref _deletecommand, value);

        }
      
        #endregion

    }
}
