using Caliburn.Micro;
using RetailWPFUserInterface.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _apiHelper;
        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public string username
        {
            get { return _username; }
            set { 
                _username = value;
                NotifyOfPropertyChange(() => username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string password
        {
            get { return _password; }
            set { 
                _password = value;
                NotifyOfPropertyChange(() => password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        //public bool CanLogin(string username, string password)
        //{
        //    return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password);
        //}
        public bool CanLogin
        {
            get
            {
                return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password);
            }
        }

        public async Task Login()
        {
            var result = await _apiHelper.Authenticate(username, password);

        }


    }
}
