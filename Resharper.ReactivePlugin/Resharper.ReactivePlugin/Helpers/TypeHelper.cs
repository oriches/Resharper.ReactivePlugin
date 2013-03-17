namespace Resharper.ReactivePlugin.Helpers
{
    using System.Linq;
    using JetBrains.ReSharper.Psi;

    public static class TypeHelper
    {
        public static bool IsIObservableType(IType type)
        {
            var scalarType = type.GetScalarType();
            if (scalarType == null)
            {
                return false;
            }

            return scalarType.GetClrName().FullName == Constants.ObservableInterfaceName;
        }

        public static bool HasIObservableSuperType(IType type)
        {
            var scalarType = type.GetScalarType();
            if (scalarType == null)
            {
                return false;
            }

            return scalarType.GetClrName().FullName == Constants.ObservableInterfaceName ||
                   scalarType.GetSuperTypes().Any(HasIObservableSuperType);
        }

        public static bool HasISchedulerSuperType(IType type)
        {
            var scalarType = type.GetScalarType();
            if (scalarType == null)
            {
                return false;
            }

            return scalarType.GetClrName().FullName == Constants.SchedulerInterfaceName ||
                   scalarType.GetSuperTypes().Any(HasISchedulerSuperType);
        }
    }
}