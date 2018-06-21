using System;
namespace Example.Models.Constraints
{
    /// <summary>
    /// This is the interface that must be implemented by navigation item constraints.
    /// A Constraint is a condition that must be <see langword="true"/> for the service to be usable.
    /// </summary>
    public interface IConstraint
    {
        bool Check();
    }
}
