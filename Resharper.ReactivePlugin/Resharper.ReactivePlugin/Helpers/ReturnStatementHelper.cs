namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Parsing;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    public static class ReturnStatementHelper
    {
        private const string AsObservableName = "AsObservable";
        private const string ObservableInterfaceName = "System.IObservable`1";
        private const string ObjectName = "System.Object";

        public static bool IsReturnTypeAsObservable(IReturnStatement returnStatement)
        {
            try
            {
                var invocationExpression = returnStatement.Value as IInvocationExpression;
                if (invocationExpression == null)
                {
                    return false;
                }
                
                var invokedExpression = invocationExpression.InvokedExpression as IReferenceExpression;
                if (invokedExpression == null)
                {
                    return false;
                }

                var resolveResult = invokedExpression.Reference.Resolve();
                var declaredElement = resolveResult.DeclaredElement;
                if (declaredElement == null)
                {
                    return false;
                }

                return declaredElement.ShortName == AsObservableName;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsReturnTypeOnlyIObservable(IReturnStatement returnStatement)
        {
            try
            {
                var type = returnStatement.Value.GetExpressionType().ToIType();
                if (type == null)
                {
                    return false;
                }

                var scalarType = type.GetScalarType();
                if (scalarType == null)
                {
                    return false;
                }

                if (scalarType.GetClrName().FullName == ObservableInterfaceName)
                {
                    if (scalarType.GetSuperTypes().Count() == 1 &&
                        scalarType.GetSuperTypes().First().GetClrName().FullName == ObjectName)
                    {

                        return true;
                    }
                }

                return false;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsReturnTypeIObservable(IReturnStatement returnStatement)
        {
            try
            {
                var type = returnStatement.Value.GetExpressionType().ToIType();
                if (type == null)
                {
                    return false;
                }

                var scalarType = type.GetScalarType();
                if (scalarType == null)
                {
                    return false;
                }

                return scalarType.GetClrName().FullName == ObservableInterfaceName ||
                       TypeHelper.HasIObservableSuperType(scalarType);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool HasPublicModifier(IReturnStatement returnStatement)
        {
            try
            {
                var memberDeclarations = returnStatement.GetContainingTypeMemberDeclaration();
                return memberDeclarations.ModifiersList.Modifiers.Any(m => m.GetTokenType() == CSharpTokenType.PUBLIC_KEYWORD);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }
    }
}