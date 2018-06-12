using System;
using System.Linq;
using System.Collections.Generic;
using Example.Security;
using AeroGear.Mobile.Security;
using AeroGear.Mobile.Core.Utils;
using Example.Android.Resources;
using SecurityChecksEnum = AeroGear.Mobile.Security.SecurityChecks;

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
                        securityCheckFactory.create(SecurityChecksEnum.BACKUP_DISALLOWED),
                        SecurityResources.device_backup_not_detected,
                        SecurityResources.device_backup_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.DEVELOPER_MODE_DISABLED),
                        SecurityResources.device_developermode_not_detected,
                        SecurityResources.device_developermode_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.ENCRYPTION),
                        SecurityResources.device_encryption_detected,
                        SecurityResources.device_encryption_not_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.NO_DEBUGGER),
                        SecurityResources.device_debugger_not_detected,
                        SecurityResources.device_debugger_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.NOT_ROOTED),
                        SecurityResources.device_root_not_detected,
                        SecurityResources.device_root_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.NOT_IN_EMULATOR),
                        SecurityResources.device_emulator_not_detected,
                        SecurityResources.device_emulator_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.SCREEN_LOCK),
                        SecurityResources.device_lock_detected,
                        SecurityResources.device_lock_not_detected
                    ));

                return securityChecks;
            }
        }
    }
}
