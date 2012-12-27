namespace Resharper.ReactivePlugin.Helpers
{
    using System.Linq;
    using JetBrains.ReSharper.Psi;

    public static class TypeHelper
    {
        private const string ObservableInterfaceName = "System.IObservable`1";
        private const string SchedulerInterfaceName = "System.Reactive.Concurrency.IScheduler";

        public static bool HasIObservableSuperType(IType type)
        {
            var scalarType = type.GetScalarType();
            if (scalarType == null)
            {
                return false;
            }

            return scalarType.GetClrName().FullName == ObservableInterfaceName ||
                   scalarType.GetSuperTypes().Any(HasIObservableSuperType);
        }

        public static bool HasISchedulerSuperType(IType type)
        {
            var scalarType = type.GetScalarType();
            if (scalarType == null)
            {
                return false;
            }

            return scalarType.GetClrName().FullName == SchedulerInterfaceName ||
                   scalarType.GetSuperTypes().Any(HasISchedulerSuperType);
        }
    }
}