using System;
using System.Collections.Generic;
using AeroGear.Mobile.Security;
using Example.Security;

[assembly: Xamarin.Forms.Dependency(typeof(Example.iOS.Security.SecurityCheckProvider))]
namespace Example.iOS.Security
{
    public class SecurityCheckProvider : ISecurityCheckProvider
    {
        public SecurityCheckProvider()
        {
        }

        public IList<ISecurityCheckType> SecurityChecks => new List<ISecurityCheckType> {
            AeroGear.Mobile.Security.SecurityChecks.NOT_JAILBROKEN,
            AeroGear.Mobile.Security.SecurityChecks.DEVICE_LOCK,
            AeroGear.Mobile.Security.SecurityChecks.NOT_IN_EMULATOR,
            AeroGear.Mobile.Security.SecurityChecks.NO_DEBUGGER
        };
    }
}
