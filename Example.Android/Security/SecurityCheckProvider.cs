using System;
using System.Linq;
using System.Collections.Generic;
using Example.Security;
using AeroGear.Mobile.Security;
using AeroGear.Mobile.Core.Utils;

[assembly: Xamarin.Forms.Dependency(typeof(Example.Android.Security.SecurityCheckProvider))]
namespace Example.Android.Security
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
                        securityCheckFactory.create("BackupDisallowedCheck"),
                        "App Data Backup Recovery Disabled",
                        "App Data Backup Recovery Enabled"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("DeveloperModeDisabledCheck"),
                        "Developer Options Disabled",
                        "Developer Options Enabled"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("EncryptionCheck"),
                        "Device Encryption Active",
                        "Device Encryption Inactive"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("NoDebuggerCheck"),
                        "No Debugger Detected",
                        "Debugger Detected"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("NonRootedCheck"),
                        "No Root Access Detected",
                        "Root Access Detected"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("NotInEmulatorCheck"),
                        "No Emulator Access Detected",
                        "Emulator Access Detected"
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create("ScreenLockCheck"),
                        "Device Lock Enabled",
                        "No Device Lock Enabled"
                    ));

                return securityChecks;
            }
        }
    }
}
