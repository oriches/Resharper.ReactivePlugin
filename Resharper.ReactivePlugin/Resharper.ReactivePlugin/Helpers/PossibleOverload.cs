namespace Resharper.ReactivePlugin.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.ReSharper.Psi;

    public sealed class PossibleOverload
    {
        private readonly ParameterWrapper[] _originalParameters;
        private readonly ParameterWrapper[] _overloadParameters;

        public IMethod OriginalMethod { get; private set; }
        public IMethod OverloadMethod { get; private set; }

        public PossibleOverload(IMethod originalMethod, IMethod overloadMethod)
        {
            OriginalMethod = originalMethod;
            OverloadMethod = overloadMethod;

            _originalParameters = OriginalMethod.Parameters.Select(p => new ParameterWrapper(p)).ToArray();
            _overloadParameters = OverloadMethod.Parameters.Select(p => new ParameterWrapper(p)).ToArray();
        }

        public IEnumerable<ParameterWrapper> OriginalParameters
        {
            get { return _originalParameters; }
        }

        public IEnumerable<ParameterWrapper> OverloadParameters
        {
            get { return _overloadParameters; }
        }
    }
}