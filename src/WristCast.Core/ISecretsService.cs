using System.Threading.Tasks;

namespace WristCast.Core
{
    public interface ISecretsService
    {
        Task SaveSecretAsync(string alias,string value);
        Task DeleteSecretAsync(string alias);
        string ReadSecretAsync(string alias);
    }
}