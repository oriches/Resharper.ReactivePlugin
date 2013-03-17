namespace Resharper.ReactivePlugin
{
    public static class Constants
    {
        // Assemblies...
        public const string ReactiveLinqAssemblyFullName = "System.Reactive.Linq.Observable";

        // Interfaces...
        public const string ObservableInterfaceName = "System.IObservable`1";
        public const string SchedulerInterfaceName = "System.Reactive.Concurrency.IScheduler";
        
        // Methods...
        public const string AsObservableName = "AsObservable";
        public const string MergeMethodName = "Merge";
        public const string SelectMethodName = "Select";
        public const string ObjectName = "System.Object";

        // Operators...
        public const string NewOperator = "new ";

        // Highlighting constants...
        public const int HighlightLength = 3;
    }
}
