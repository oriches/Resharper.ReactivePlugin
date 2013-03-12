namespace Resharper.ReactivePlugin.ProblemAnalyzers
{
    using JetBrains.ReSharper.Psi;

    public sealed class CurrentAndPreviousState<T>
    {
        private IPsiSourceFile _currentFile;

        public T Previous { get; private set; }
        public T Current { get; private set; }

        public void InitialiseForFile(IPsiSourceFile file)
        {
            var currentName = _currentFile == null ? null : _currentFile.Name;
            if (!Equals(currentName, file.Name))
            {
                Reset();
            }

            _currentFile = file;
        }

        public void SetCurrent(T current)
        {
            Previous = Current;
            Current = current;
        }

        public void Reset()
        {
            Previous = default(T);
            Current = default(T);
        }
    }
}