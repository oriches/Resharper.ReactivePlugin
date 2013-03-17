namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Parsing;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    public static class PropertyHelper
    {
        public static bool IsPropertyAssignment(IAssignmentExpression expression, out IProperty property)
        {
            property = null;
            try
            {
                var referenceExpression = expression.Dest as IReferenceExpression;
                if (referenceExpression == null)
                {
                    return false;
                }

                var result = referenceExpression.Reference.Resolve().Result;
                property = result.DeclaredElement as IProperty;
                return property != null;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsAutoProperty(IProperty property)
        {
            try
            {
                var declaration = property.GetDeclarations().FirstOrDefault();
                if (declaration == null)
                {
                    return false;
                }

                var propertyDeclaration = declaration as IPropertyDeclaration;
                if (propertyDeclaration == null)
                {
                    return false;
                }

                return propertyDeclaration.IsAuto;
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool HasPublicModifier(IProperty property)
        {
            try
            {
                if (property.Getter == null)
                {
                    return false;
                }

                var declaration = property.GetDeclarations().FirstOrDefault();
                if (declaration == null)
                {
                    return false;
                }

                var memberDeclaration = declaration as ICSharpTypeMemberDeclaration;
                if (memberDeclaration == null)
                {
                    return false;
                }

                return memberDeclaration.ModifiersList.Modifiers.Any(m => m.GetTokenType() == CSharpTokenType.PUBLIC_KEYWORD);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool IsSourceAssignmentAsObservable(IAssignmentExpression expression)
        {
            try
            {
                var invocationExpression =  expression.Source as IInvocationExpression;
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
    }
}