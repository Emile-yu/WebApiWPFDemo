using Microsoft.AspNet.Identity;
using RetailDataManager.Library.DataAccess;
using RetailDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RetailDataManager.Controllers
{
    [Authorize(Roles = "Cashier")]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get()
        {
            //string userId = RequestContext.Principal.Identity.GetUserId();

            ProductData data = new ProductData();

            return data.GetProducts();
        }
    }
}
