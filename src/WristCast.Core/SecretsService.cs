using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RestSharp.Authenticators;
using Tizen.Security.SecureRepository;
using WristCast.Core.IoC;

namespace WristCast.Core
{
    public interface ISecretsService
    {
        Task SaveSecretAsync(string alias,string value);
        Task DeleteSecretAsync(string alias);
        string ReadSecretAsync(string alias);
    }

    public static class Secrets
    {
        public const string KeyAlias = "ListenNotesApiKey";

        private static readonly Lazy<ISecretsService> _sercive = new Lazy<ISecretsService>(GetService);

        private static ISecretsService GetService()
        {
            return IocContainer.Instance.Resolve<ISecretsService>();
        }

        public static string ApiKey => _sercive.Value.ReadSecretAsync(KeyAlias);
    }

    public class SecretsService : ISecretsService
    {
        public string ReadSecretAsync(string alias)
        {
            try
            {
                return Encoding.UTF8.GetString(DataManager.Get(alias, null));
            }
            catch (Exception e)
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
