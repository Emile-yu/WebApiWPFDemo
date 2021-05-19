using Caliburn.Micro;
using RetailWPFUserInterface.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _eventAggregator;
        private SalesViewModel _salesVM;
        private SimpleContainer _container;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM, SimpleContainer container)
        {
            _salesVM = salesVM;
            _container = container;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
            //refresh the loginviewModel per request
            //ActivateItem(_container.GetInstance<LoginViewModel>());
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}
