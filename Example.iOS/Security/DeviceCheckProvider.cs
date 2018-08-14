using System;
using System.Linq;
using System.Collections.Generic;
using AeroGear.Mobile.Security;
using Example.Security;
using AeroGear.Mobile.Core.Utils;
using Example.iOS.Resources;
using SecurityChecksEnum = AeroGear.Mobile.Security.DeviceChecks;

[assembly: Xamarin.Forms.Dependency(typeof(Example.iOS.Security.DeviceCheckProvider))]
namespace Example.iOS.Security
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
                        deviceCheckFactory.create(SecurityChecksEnum.DEVICE_LOCK_ENABLED),
                        SecurityResources.device_lock_detected,
                        SecurityResources.device_lock_not_detected
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.DEBUGGER_ATTACHED),
                        SecurityResources.device_debugger_not_detected,
                        SecurityResources.device_debugger_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.JAILBREAK_ENABLED),
                        SecurityResources.device_jailbreak_not_detected,
                        SecurityResources.device_jailbreak_detected,
                        true
                    ));
                deviceChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        deviceCheckFactory.create(SecurityChecksEnum.IS_EMULATOR),
                        SecurityResources.device_emulator_not_detected,
                        SecurityResources.device_emulator_detected,
                        true
                    ));

                return deviceChecks;
            }
        }
    }
}
