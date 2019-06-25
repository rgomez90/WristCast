using System.Collections.Generic;

namespace WristCast
{
    public interface IAppPrivacyPermissions
    {
        IEnumerable<string> Permissions { get; }
    }
}