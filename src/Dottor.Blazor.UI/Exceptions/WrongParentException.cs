namespace Dottor.Blazor.UI.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WrongParentException : Exception
    {
        public WrongParentException(Type componentType, Type parentType)
        {
            ComponentType = componentType;
            ParentType = parentType;
        }

        public Type ComponentType { get; set; }

        public Type ParentType { get; set; }

        public override string Message => $"Componennt {ComponentType} must be in a {ParentType}";
    }
}
