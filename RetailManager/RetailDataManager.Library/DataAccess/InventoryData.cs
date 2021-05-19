using RetailDataManager.Library.DBUtility;
using RetailDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDataManager.Library.DataAccess
{
    public class InventoryData
    {
        public void SaveInventory(InventoryModel inventory)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spInventory_Insert", inventory, "TRMData");
        }

        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "TRMData");

            return output;
        }
    }
}
