using Caliburn.Micro;
using RetailWPFUserInterface.EventModels;
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
        private string _username = "tim@iamtimcoreyc.com";
        private string _password = "Pwd12345.";
        private IAPIHelper _apiHelper;
        private IEventAggregator _eventAggregator;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            this._eventAggregator = eventAggregator;
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
        public bool IsErrorVisible
        {
            get 
            {
                return !String.IsNullOrWhiteSpace(ErrorMessage); 
            }
            
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { 
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
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
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(username, password);

                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                _eventAggregator.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }


    }
}
