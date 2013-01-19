namespace Resharper.ReactivePlugin.CodeCleanup
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using Helpers;
    using JetBrains.Application;
    using JetBrains.Application.Progress;
    using JetBrains.DocumentModel;
    using JetBrains.ReSharper.Feature.Services.CodeCleanup;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
    using JetBrains.ReSharper.Psi.Tree;
    using CodeCleanup = JetBrains.ReSharper.Feature.Services.CodeCleanup.CodeCleanup;

    [CodeCleanupModule]
    public sealed class UseSelectManyInsteadOfMerge : ICodeCleanupModule
    {
        private static readonly Descriptor DescriptorInstance = new Descriptor();
        private readonly IShellLocks _shellLocks;

        public UseSelectManyInsteadOfMerge(IShellLocks shellLocks)
        {
          _shellLocks = shellLocks;
        }

        public PsiLanguageType LanguageType
        {
            get { return CSharpLanguage.Instance; }
        }

        public ICollection<CodeCleanupOptionDescriptor> Descriptors
        {
            get { return new CodeCleanupOptionDescriptor[] { DescriptorInstance }; }
        }

        public bool IsAvailableOnSelection
        {
            get { return false; }
        }

        public void SetDefaultSetting(CodeCleanupProfile profile, CodeCleanup.DefaultProfileType profileType)
        {
            switch (profileType)
            {
                case CodeCleanup.DefaultProfileType.FULL:
                    profile.SetSetting(DescriptorInstance, true);
                    break;

                case CodeCleanup.DefaultProfileType.REFORMAT:
                    profile.SetSetting(DescriptorInstance, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("profileType");
            }
        }

        public bool IsAvailable(IPsiSourceFile sourceFile)
        {
            return sourceFile.GetNonInjectedPsiFile<CSharpLanguage>() != null;
        }

        public void Process(IPsiSourceFile sourceFile, IRangeMarker rangeMarker, CodeCleanupProfile profile,
                            IProgressIndicator progressIndicator)
        {
            var file = sourceFile.GetNonInjectedPsiFile<CSharpLanguage>();
            if (file == null)
                return;

            if (!profile.GetSetting(DescriptorInstance))
                return;

            CSharpElementFactory elementFactory = CSharpElementFactory.GetInstance(sourceFile.PsiModule);

            file.GetPsiServices().PsiManager.DoTransaction(
            () =>
            {
                using (_shellLocks.UsingWriteLock())
                {
                    InitialiseState();

                    file.ProcessChildren<IExpression>(
                        expression =>
                        {
                            Debug.WriteLine(expression.GetText());

                            var tmp1 = expression as IInvocationExpression;
                            if (tmp1 != null)
                            {
                                var tmp2 = tmp1.Reference;
                                var tmp3 = tmp2.Invocation.Reference.Resolve();
                                var tmp4 = tmp2.Resolve();
                            }

                            var tmp5 = expression as IReferenceExpression;
                            if (tmp5 != null)
                            {
                                var tmp6 = tmp5.Reference.Resolve();
                                var tmp7 = tmp5.Type().GetScalarType();
                                if (tmp7 != null)
                                {
                                    var tmp8 = tmp7.Resolve();
                                }
                            }

                            Debug.WriteLine("Made it here...");

//
//                            IMethod newMethod;
//                            if (!MethodHelper.IsMethod(expression, out newMethod))
//                            {
//                                return;
//                            }
//
//                            Debug.WriteLine("NewMethod = " + newMethod.ShortName);
//
//                            _nextMethod = _currentMethod;
//                            _currentMethod = newMethod;
//
//                            if (_currentMethod == null || _nextMethod == null)
//                            {
//                                return;
//                            }
//
//                            if (!MethodHelper.IsReturnTypeIObservable(_currentMethod) ||
//                                !MethodHelper.IsReturnTypeIObservable(_nextMethod))
//                            {
//                                return;
//                            }

                            //Debug.WriteLine(_currentMethod.ShortName);
                            //Debug.WriteLine(_nextMethod.ShortName);
                            //Debug.WriteLine("------------------------------------------");

                            //                            var value = expression.ConstantValue;
                            //                            if (value.IsInteger() && Convert.ToInt32(value.Value) == int.MaxValue)
                            //                                ModificationUtil.ReplaceChild(expression, elementFactory.CreateExpression("int.MaxValue"));
                        });
                }
            },
            "Code cleanup");
        }

        private IMethod _currentMethod;
        private IMethod _nextMethod;

        private void InitialiseState()
        {
            _currentMethod = null;
            _nextMethod = null;
        }

        [DefaultValue(false)]
        [DisplayName("Use SelectMany instead of Select & Merge")]
        [Category(CSharpCategory)]
        private class Descriptor : CodeCleanupBoolOptionDescriptor
        {
            public Descriptor()
                : base("UseSelectMany")
            {
            }
        }
    }
}
