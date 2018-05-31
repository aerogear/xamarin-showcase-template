using System;
using System.Linq;
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

        public IList<ISecurityCheckType> SecurityChecks
        {
            get
            {
                return AeroGear.Mobile.Security.SecurityChecks.GetAllChecks().ToList<ISecurityCheckType>();
            }
        }
    }
}
