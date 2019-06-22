using System;
using Autofac;
using WristCast.Core.IoC;

namespace WristCast.Core
{
    public static class Secrets
    {
        public const string KeyAlias = "ListenNotesApiKey";

        private static readonly Lazy<ISecretsService> Sercive = new Lazy<ISecretsService>(GetService);

        private static ISecretsService GetService()
        {
            return IocContainer.Instance.Resolve<ISecretsService>();
        }

        public static string ApiKey => Sercive.Value.ReadSecretAsync(KeyAlias);
    }
}