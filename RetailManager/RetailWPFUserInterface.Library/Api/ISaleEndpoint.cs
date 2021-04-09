using RetailWPFUserInterface.Library.Model;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}