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

                return declaredElement.ShortName == Constants.AsObservableName;
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

                if (scalarType.GetClrName().FullName == Constants.ObservableInterfaceName)
                {
                    if (scalarType.GetSuperTypes().Count() == 1 &&
                        scalarType.GetSuperTypes().First().GetClrName().FullName == Constants.ObjectName)
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

                return scalarType.GetClrName().FullName == Constants.ObservableInterfaceName ||
                       TypeHelper.HasIObservableSuperType(scalarType);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsContainingDeclarationPublic(IReturnStatement returnStatement)
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

        public static bool IsContainingDeclarationReturnTypeIObservable(IReturnStatement returnStatement)
        {
            try
            {
                var declaredElement = returnStatement.GetContainingTypeMemberDeclaration().DeclaredElement;

                IDeclaredType scalarType = null;
                var property = declaredElement as IProperty;
                var method = declaredElement as IMethod;

                if (property != null)
                {
                    scalarType = property.Type.GetScalarType();
                }
                else if (method != null)
                {
                    scalarType = method.ReturnType.GetScalarType();
                }

                if (scalarType == null)
                {
                    return false;
                }

                if (scalarType.GetClrName().FullName != Constants.ObservableInterfaceName)
                {
                    return false;
                }
                
                return true;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }
    }
}