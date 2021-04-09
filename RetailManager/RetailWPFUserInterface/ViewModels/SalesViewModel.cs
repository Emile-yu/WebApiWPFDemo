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

        private ProductModel _selectedProduct; 

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        private BindableCollection<CartItemModel> _cart = new BindableCollection<CartItemModel>();

        public BindableCollection<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                //if overwrite this bind, it wouble a problem,so add this ligne
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity = 1;
        private IProductEndpoint _productEndpoint;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set { 
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get
            {
                decimal subTotal = 0;

                foreach (var item in Cart)
                {
                    subTotal += item.Product.RetailPrice * item.QuantityInCart;
                }
                //It's going to convert a currency
                return subTotal.ToString("C");
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
                bool output = false;

                //when first load this form , SelectedProduct will be null util you select something
                //we can dentify if SelectedProduct is null or not
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock > ItemQuantity)
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }

            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            //how to refresh th cart display

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
