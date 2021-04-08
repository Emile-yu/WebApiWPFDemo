using RetailWPFUserInterface.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}