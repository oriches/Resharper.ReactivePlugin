namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;

    public static class SchedulerHelper
    {
        private const string SchedulerInterfaceName = "System.Reactive.Concurrency.IScheduler";

        public static bool HasPopulatedSchedulerParameter<T>(T owner, IArgumentList argumentList) where T : IParametersOwner
        {
            ICSharpArgument schedulerArgument;
            return HasPopulatedSchedulerParameter(owner, argumentList, out schedulerArgument);
        }

        public static bool HasPopulatedSchedulerParameter<T>(T owner, IArgumentList argumentList, out ICSharpArgument schedulerArgument) where T : IParametersOwner
        {
            schedulerArgument = null;

            try
            {
                IParameter schedulerParameter;
                if (!DoesParametersContainSchedulerInterface(owner.Parameters, out schedulerParameter))
                {
                    return false;
                }

                return !schedulerParameter.IsOptional ||
                       argumentList.Arguments.Any(a => TypeHelper.HasISchedulerSuperType(a.Value.Type()));
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool HasSchedulerParameter<T>(T owner) where T : IParametersOwner
        {
            try
            {
                return DoesParametersContainSchedulerInterface(owner.Parameters);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool HasOverloadWithSchedulerParameter(IMethod method)
        {
            try
            {
                var typeElement = method.GetContainingType();
                if (typeElement == null)
                {
                    return false;
                }

                return typeElement.GetMembers().Where(m => m.ShortName == method.ShortName)
                                  .OfType<IMethod>()
                                  .Select(memberMethod => DoesParametersContainSchedulerInterface(memberMethod.Parameters))
                                  .Any(hasScheduler => hasScheduler);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        public static bool HasOverloadWithSchedulerParameter(IConstructor constructor)
        {
            try
            {
                var typeElement = constructor.GetContainingType();
                if (typeElement == null)
                {
                    return false;
                }

                return typeElement.Constructors.Where(c => c.ShortName == constructor.ShortName)
                                  .Select(constructorMethod => DoesParametersContainSchedulerInterface(constructorMethod.Parameters))
                                  .Any(hasScheduler => hasScheduler);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }
        
        private static bool DoesParametersContainSchedulerInterface(IEnumerable<IParameter> parameters)
        {
            IParameter schedulerParameter;
            return DoesParametersContainSchedulerInterface(parameters, out schedulerParameter);
        }

        private static bool DoesParametersContainSchedulerInterface(IEnumerable<IParameter> parameters, out IParameter schedulerParameter)
        {
            schedulerParameter = parameters.SingleOrDefault(parameter => ((IDeclaredType)parameter.Type).GetClrName().FullName == SchedulerInterfaceName);

            return schedulerParameter != null;
        }
    }
}