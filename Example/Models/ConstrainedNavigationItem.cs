using System;
using Example.Models.Constraints;
using Xamarin.Forms;

namespace Example.Models
{
    /// <summary>
    /// Base class for constrained service pages.
    /// </summary>
    public abstract class ConstrainedNavigationItem : NavigationItem
    {
        private readonly IConstraint[] constraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Example.Models.ConstrainedNavigationItem"/> class.
        /// </summary>
        /// <param name="constraints">The list of constraints that must be satisfied for considering this service ready to be used.</param>
        public ConstrainedNavigationItem(params IConstraint[] constraints)
        {
            this.constraints = constraints;
        }

        private bool Check()
        {
            foreach(IConstraint constraint in constraints)
            {
                if (!constraint.Check())
                {
                    return false;
                }
            }

            return true;
        }

        public override Page Page
        {
            get
            {
                if (Check())
                {
                    return BuildPage();
                }

                return null;
            }
        }

        /// <summary>
        /// This must be implemented by subclasses to return the page to be visualised if the constrains are satisfied.
        /// </summary>
        /// <returns>The page to be visualised if the constrains are satisfied..</returns>
        protected abstract Page BuildPage();
    }
}
