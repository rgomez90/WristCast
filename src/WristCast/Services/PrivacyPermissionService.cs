using System;
using System.Threading.Tasks;
using Tizen.Security;
using Xamarin.Forms;

namespace WristCast.Services
{
    public class PrivacyPermissionService : IPrivacyPermissionService
    {
        TaskCompletionSource<bool> _tcs;
        int _grantedPermission;

        /// <summary>
        /// Request user permission for privacy privileges
        /// </summary>
        /// <param name="service">privacy privilege</param>
        /// <returns>true if user consent is gained</returns>
        public async Task<bool> GetPermission(string service)
        {
            try
            {
                _grantedPermission = -1;
                // Gets the status of a privacy privilege permission.
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(service);
                switch (result)
                {
                    case CheckResult.Allow:
                        // user consent for privacy privilege is already gained
                        return true;
                    case CheckResult.Deny:
                    case CheckResult.Ask:
                        // User permission request should be required
                        _tcs = new TaskCompletionSource<bool>();
                        // Gets the response context for a given privilege.
                        var reponseContext = PrivacyPrivilegeManager.GetResponseContext(service);
                        PrivacyPrivilegeManager.ResponseContext context = null;
                        if (reponseContext.TryGetTarget(out context))
                        {
                            if (context != null)
                            {
                                context.ResponseFetched += Context_ResponseFetched;
                            }
                        }
                        // Try to get the permission for service from a user.
                        PrivacyPrivilegeManager.RequestPermission(service);

                        // Check if permission is granted or not every second
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), CheckPermission);
                        });

                        return await _tcs.Task;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[UserPermission] (" + service + ") Error :" + e.Message);
                return false;
            }
        }

        bool CheckPermission()
        {
            switch (_grantedPermission)
            {
                // In case that an app user doesn't give permission yet.
                case -1:
                    return true;
                // In case that an app user gives permission.
                // Denied
                // Allowed
                case 0:
                    _tcs.SetResult(false);
                    break;
                default:
                    _tcs.SetResult(true);
                    break;
            }

            return false;
        }

        // Invoked when an app user responses for the permission request
        private void Context_ResponseFetched(object sender, RequestResponseEventArgs e)
        {
            if (e.result == RequestResult.AllowForever)
            {
                // User allows an app to grant privacy privilege
                _grantedPermission = 1;
            }
            else
            {
                // User doesn't allow an app to grant privacy privilege
                _grantedPermission = 0;
            }
        }
    }
}

