using System.Collections.Generic;

namespace WristCast.Services
{
    class TizenAppPrivacyPermissions : IAppPrivacyPermissions
    {
        public TizenAppPrivacyPermissions()
        {
            Permissions = new[]
            {
                "http://tizen.org/privilege/mediastorage",
                "http://tizen.org/privilege/externalstorage"
            };
        }

        public IEnumerable<string> Permissions { get; }
    }
}