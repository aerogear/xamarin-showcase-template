using System;
using System.Linq;
using System.Collections.Generic;
using AeroGear.Mobile.Security;
using Example.Security;
using AeroGear.Mobile.Core.Utils;

[assembly: Xamarin.Forms.Dependency(typeof(Example.iOS.Security.SecurityCheckProvider))]
namespace Example.iOS.Security
{
    public class SecurityCheckProvider : ISecurityCheckProvider
    {
        public SecurityCheckProvider()
        {
        }

        public IList<ISecurityCheck> SecurityChecks
        {
            get
            {
                ISecurityCheckFactory securityCheckFactory = ServiceFinder.Resolve<ISecurityCheckFactory>();
                var securityChecks = new List<ISecurityCheck>();

                // Configure securitychecks list
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("DeviceLockCheck"),
                        "Device Lock Enabled",
                        "No Device Lock Enabled"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("NoDebuggerCheck"),
                        "No Debugger Detected",
                        "Debugger Detected"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("NonJailbrokenCheck"),
                        "No Jailbreak Detected",
                        "Jailbreak Detected"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("NotInEmulatorCheck"),
                        "No Emulator Access Detected",
                        "Emulator Access Detected"
                    ));

                return securityChecks;
            }
        }
    }
}
