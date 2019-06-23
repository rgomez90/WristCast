using System;
using Autofac;
using WristCast.Core.IoC;
using WristCast.Core.Services;

namespace WristCast.Core
{
    public static class Secrets
    {
        public const string KeyAlias = "ListenNotesApiKey";

        private static readonly Lazy<ISecretsService> Service = new Lazy<ISecretsService>(GetService);

        private static ISecretsService GetService()
        {
            return IocContainer.Instance.Resolve<ISecretsService>();
        }

        public static string ApiKey => Service.Value.ReadSecretAsync(KeyAlias);
    }
}