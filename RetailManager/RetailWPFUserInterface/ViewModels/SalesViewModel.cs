using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindableCollection<string> _products;

        public BindableCollection<string> Products
        {
            get { return _products; }
            set { 
                _products = value;
                //if overwrite this bind, it wouble a problem,so add this ligne
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindableCollection<string> _cart;

        public BindableCollection<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                //if overwrite this bind, it wouble a problem,so add this ligne
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private string _itemQuantity;

        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set { 
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string SubTotal
        {
            get
            {
                return $"0.00";
            }
        }

        public string Total
        {
            get
            {
                return $"0.00";
            }
        }

        public string Tax
        {
            get
            {
                return $"0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ItemQuantity);
            }
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ItemQuantity);
            }
        }

        public void AddRemoveFromCart()
        {

        }


        public bool CanCheckOut
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ItemQuantity);
            }
        }

        public void CheckOut()
        {

        }

    }
}
