using System;
using System.Linq;
using System.Collections.Generic;
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

        public IList<ISecurityCheckType> SecurityChecks
        {
            get
            {
                return AeroGear.Mobile.Security.SecurityChecks.GetAllChecks().ToList<ISecurityCheckType>();
            }
        }
    }
}
