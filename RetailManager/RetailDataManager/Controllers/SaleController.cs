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
    [Authorize]
    public class SaleController : ApiController
    {
        [Authorize(Roles ="Cashier")]
        public void Post(SaleModel sale)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            SaleData data = new SaleData();

            data.SaveSale(sale, userId);
        }
        [Authorize(Roles = "Admin, Manger")]//or
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData();

            return data.GetSaleReport();
        }
        //public List<ProductModel> Get()
        //{
        //    //string userId = RequestContext.Principal.Identity.GetUserId();

        //    ProductData data = new ProductData();

        //    return data.GetProducts();
        //}
    }
}
