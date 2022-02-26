using Caliburn.Micro;
using RetailWPFUserInterface.EventModels;
using RetailWPFUserInterface.Library.Api;
using RetailWPFUserInterface.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetailWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        public IEventAggregator _eventAggregator { get; set; }
        private SalesViewModel _salesVM;
        private SimpleContainer _container;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;

        public ShellViewModel(SalesViewModel salesVM, SimpleContainer container, IEventAggregator eventAggregator,
                              ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            
            _salesVM = salesVM;
            _container = container;
            this._user = user;
            this._apiHelper = apiHelper;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
            //refresh the loginviewModel per request
            //ActivateItem(_container.GetInstance<LoginViewModel>());
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public bool IsLoggedIn 
        {
            get
            {
                bool output = false;

                if (!String.IsNullOrWhiteSpace(_user.Token))
                {
                    output = true;
                }
                return output;
            }
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void UserManagerment()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
