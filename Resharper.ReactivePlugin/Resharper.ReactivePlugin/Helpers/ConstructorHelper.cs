namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    public static class ConstructorHelper
    {
        private const string ObservableInterfaceName = "System.IObservable`1";

        public static bool IsContructor(IObjectCreationExpression creationExpression, out IConstructor constructor)
        {
            constructor = null;

            try
            {
                if (creationExpression.Reference == null)
                {
                    return false;
                }

                var resolveResult = creationExpression.Reference.Resolve();
                constructor = resolveResult.DeclaredElement as IConstructor;

                return constructor != null;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsReturnTypeIObservable(IConstructor constructor)
        {
            try
            {
                var declaredType = constructor.ReturnType as IDeclaredType;
                if (declaredType == null)
                {
                    return false;
                }

                if (declaredType.Assembly == null)
                {
                    return false;
                }

                var typeElement = constructor.GetContainingType();
                if (typeElement == null)
                {
                    return false;
                }

                return typeElement.GetSuperTypes()
                    .Select(t => t.GetClrName().FullName)
                    .Any(n => n == ObservableInterfaceName);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }
    }
}