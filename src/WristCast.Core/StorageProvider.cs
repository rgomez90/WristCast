using Autofac;
using WristCast.Core.IoC;
using WristCast.Core.Services;

namespace WristCast.Core
{
    public class StorageProvider
    {
        private static IStorageProvider _current;

        public static IStorageProvider Current =>
            _current ?? (_current = IocContainer.Instance.Resolve<IStorageProvider>());
    }
}
