namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using JetBrains.ReSharper.Psi;

    public sealed class ParameterWrapper : IEquatable<ParameterWrapper>
    {
        private readonly IParameter _parameter;

        public ParameterWrapper(IParameter parameter)
        {
            _parameter = parameter;
        }

        public IParameter Parameter
        {
            get { return _parameter; }
        }

        public string Name
        {
            get
            {
                return _parameter == null ? null : _parameter.ShortName;
            }
        }

        public string TypeName
        {
            get
            {
                if (_parameter == null)
                {
                    return null;
                }

                var scalarType = _parameter.Type.GetScalarType();
                return scalarType == null ? null : scalarType.GetClrName().FullName;
            }
        }
        
        public bool Equals(ParameterWrapper other)
        {
            if (other == null)
            {
                return false;
            }

            if (Name != other.Name)
            {
                return false;
            }

            if (TypeName != other.TypeName)
            {
                return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is ParameterWrapper && Equals((ParameterWrapper) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
            }
        }

        public static bool operator ==(ParameterWrapper left, ParameterWrapper right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ParameterWrapper left, ParameterWrapper right)
        {
            return !Equals(left, right);
        }
    }
}