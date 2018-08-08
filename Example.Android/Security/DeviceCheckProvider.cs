using System;
using System.Linq;
using System.Collections.Generic;
using Example.Security;
using AeroGear.Mobile.Security;
using AeroGear.Mobile.Core.Utils;
using Example.Android.Resources;
using SecurityChecksEnum = AeroGear.Mobile.Security.DeviceChecks;

[assembly: Xamarin.Forms.Dependency(typeof(Example.Android.Security.DeviceCheckProvider))]
namespace Example.Android.Security
{
    public class DeviceCheckProvider : IDeviceCheckProvider
    {
        public DeviceCheckProvider()
        {
        }

        public IList<IDeviceCheck> DeviceChecks
        {
            get
            {
                IDeviceCheckFactory deviceCheckFactory = ServiceFinder.Resolve<IDeviceCheckFactory>();
                var deviceChecks = new List<IDeviceCheck>();

                // Configure securitychecks list
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.BACKUP_ENABLED),
                        SecurityResources.device_backup_not_detected,
                        SecurityResources.device_backup_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.DEVELOPER_MODE_ENABLED),
                        SecurityResources.device_developermode_not_detected,
                        SecurityResources.device_developermode_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.ENCRYPTION_ENABLED),
                        SecurityResources.device_encryption_detected,
                        SecurityResources.device_encryption_not_detected
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.DEBUGGER_ENABLED),
                        SecurityResources.device_debugger_not_detected,
                        SecurityResources.device_debugger_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.ROOT_ENABLED),
                        SecurityResources.device_root_not_detected,
                        SecurityResources.device_root_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.IS_EMULATOR),
                        SecurityResources.device_emulator_not_detected,
                        SecurityResources.device_emulator_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.SCREEN_LOCK_ENABLED),
                        SecurityResources.device_lock_detected,
                        SecurityResources.device_lock_not_detected
                    ));

                return deviceChecks;
            }
        }
    }
}
