using System;
using System.Linq;
using System.Collections.Generic;
using AeroGear.Mobile.Security;
using Example.Security;
using AeroGear.Mobile.Core.Utils;
using Example.iOS.Resources;
using SecurityChecksEnum = AeroGear.Mobile.Security.SecurityChecks;

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
                        securityCheckFactory.create(SecurityChecksEnum.DEVICE_LOCK),
                        SecurityResources.device_lock_detected,
                        SecurityResources.device_lock_not_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.NO_DEBUGGER),
                        SecurityResources.device_debugger_not_detected,
                        SecurityResources.device_debugger_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.NOT_JAILBROKEN),
                        SecurityResources.device_jailbreak_not_detected,
                        SecurityResources.device_jailbreak_detected
                    ));
                securityChecks.Add(
                    new DeviceCheckSecurityDecorator(
                        securityCheckFactory.create(SecurityChecksEnum.NOT_IN_EMULATOR),
                        SecurityResources.device_emulator_not_detected,
                        SecurityResources.device_emulator_detected
                    ));

                return securityChecks;
            }
        }
    }
}
