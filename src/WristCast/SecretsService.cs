using System;
using System.Text;
using System.Threading.Tasks;
using Tizen.Security.SecureRepository;
using WristCast.Core;

namespace WristCast
{
    public class SecretsService : ISecretsService
    {
        public string ReadSecretAsync(string alias)
        {
            try
            {
                return Encoding.UTF8.GetString(DataManager.Get(alias, null));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task SaveSecretAsync(string alias,string apiKey)
        {
            return Task.Run(() =>
            {
                DataManager.Save(alias, Encoding.UTF8.GetBytes(apiKey), new Policy());
            });
        }

        public Task DeleteSecretAsync(string alias)
        {
            return Task.Run(() =>
            {
                Manager.RemoveAlias(alias);
            });
        }
    }


}
