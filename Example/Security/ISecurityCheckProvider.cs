using System;
using System.Collections.Generic;
using AeroGear.Mobile.Security;

namespace Example.Security
{
    public interface ISecurityCheckProvider
    {
        ICollection<ISecurityCheckType> SecurityChecks { get; }
    }
}

