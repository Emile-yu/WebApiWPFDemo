using Caliburn.Micro;
using RetailWPFUserInterface.Library.Api;
using RetailWPFUserInterface.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.ViewModels
{
    public class SalesViewModel : Screen
    {
        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            this._productEndpoint = productEndpoint;
            
        }

        //when the load products is done, we show the viec
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindableCollection<ProductModel>(productList);
        }

        private BindableCollection<ProductModel> _products;

        public BindableCollection<ProductModel> Products
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

        private int _itemQuantity;
        private IProductEndpoint _productEndpoint;

        public int ItemQuantity
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
                return ItemQuantity > 0;
            }
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                return ItemQuantity > 0;
            }
        }

        public void AddRemoveFromCart()
        {

        }


        public bool CanCheckOut
        {
            get
            {
                return ItemQuantity > 0;
            }
        }

        public void CheckOut()
        {

        }

    }
}
