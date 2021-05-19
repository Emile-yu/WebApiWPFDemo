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
    public class InventoryController : ApiController
    {
        [Authorize(Roles = "Admin, Manger")]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData();

            return data.GetInventory();
        }

        [Authorize(Roles = "Admin")]
        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData();

            data.SaveInventory(item);
        }
    }
}
