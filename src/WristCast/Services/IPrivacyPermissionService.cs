using System.Threading.Tasks;

namespace WristCast.Services
{
    public interface IPrivacyPermissionService
    {
        Task<bool> GetPermission(string service);
    }
}