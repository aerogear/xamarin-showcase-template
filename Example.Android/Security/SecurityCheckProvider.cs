using System;
using System.Collections.Generic;
using AeroGear.Mobile.Auth.Authenticator;
using Example.Security;
using AeroGear.Mobile.Security;

[assembly: Xamarin.Forms.Dependency(typeof(Example.Android.Security.SecurityCheckProvider))]
namespace Example.Android.Security
{
    public class SecurityCheckProvider : ISecurityCheckProvider
    {
        public SecurityCheckProvider()
        {
        }

        public IList<ISecurityCheckType> SecurityChecks => new List<ISecurityCheckType> {
            AeroGear.Mobile.Security.SecurityChecks.NOT_ROOTED,
            AeroGear.Mobile.Security.SecurityChecks.SCREEN_LOCK,
            AeroGear.Mobile.Security.SecurityChecks.NOT_IN_EMULATOR,
            AeroGear.Mobile.Security.SecurityChecks.NO_DEBUGGER,
            AeroGear.Mobile.Security.SecurityChecks.BACKUP_DISALLOWED,
            AeroGear.Mobile.Security.SecurityChecks.DEVELOPER_MODE_DISABLED,
            AeroGear.Mobile.Security.SecurityChecks.ENCRYPTION
        };
    }
}
