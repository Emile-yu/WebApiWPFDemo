using RetailWPFUserInterface.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailWPFUserInterface.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task AddRole(string userId, string roleName);
        Task RemoveRole(string userId, string roleName);
    }
}