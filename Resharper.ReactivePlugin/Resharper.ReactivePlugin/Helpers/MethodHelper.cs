namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using System.Diagnostics;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Tree;

    public static class MethodHelper
    {
        public static bool IsMethod(IExpression expression, out IMethod method)
        {
            method = null;
            var invocationExpression = expression as IInvocationExpression;
            return invocationExpression != null && IsMethod(invocationExpression, out method);
        }

        public static bool IsMethod(IInvocationExpression expression, out IMethod method)
        {
            method = null;

            try
            {
                var invokedExpression = expression.InvokedExpression as IReferenceExpression;
                if (invokedExpression == null)
                {
                    return false;
                }

                var resolveResult = invokedExpression.Reference.Resolve();
                method = resolveResult.DeclaredElement as IMethod;

                return method != null;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsReturnTypeIObservable(IMethod method)
        {
            try
            {
                var declaredType = method.ReturnType as IDeclaredType;
                if (declaredType == null)
                {
                    return false;
                }

                if (declaredType.Assembly == null)
                {
                    return false;
                }

                return declaredType.GetClrName().FullName == Constants.ObservableInterfaceName;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsFromReactiveObservableClass(IMethod method)
        {
            try
            {
                var classType = method.GetContainingType();
                if (classType == null)
                {
                    return false;
                }

                return classType.GetClrName().FullName == Constants.ReactiveLinqAssemblyFullName;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }
    }
}