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

        public static bool HasOverloadWithSchedulerParameter(IMethod method, IArgumentList argumentList)
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
                                  .Any(m => DoesOverloadSupportCurrentArgumentsAndScheduler(method, argumentList, m.Parameters));
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);
                return false;
            }
        }

        private static bool DoesOverloadSupportCurrentArgumentsAndScheduler(IMethod method, IArgumentList argumentList, IList<IParameter> parameters)
        {
            // If no parameters it can't be an overload...
            if (parameters.Count == 0)
            {
                return false;
            }

            // Is the number of parameters n more than the current number of arguments (possible overload)...
            // If it's an extension method then n == 2 else n == 1
            var n = method.IsExtensionMethod ? 2 : 1;
            if ((argumentList.Arguments.Count + n) != parameters.Count)
            {
                return false;
            }

            // Is the last parameter an IScheduler (possible overload)...
            if (((IDeclaredType) parameters.Last().Type).GetClrName().FullName != SchedulerInterfaceName)
            {
                return false;
            }

            for (var i = 0; i < argumentList.Arguments.Count(); i++)
            {
                var argument = argumentList.Arguments[i];
                var argumentType = argument.Value.Type().GetScalarType();
                if (argumentType == null)
                {
                    return false;
                }

                var parameter = parameters[i];
                var parameterType = parameter.Type.GetScalarType();
                if (parameterType == null)
                {
                    return false;
                }

                if (!parameterType.IsOpenType)
                {
                    if (argumentType.GetClrName().FullName != parameterType.GetClrName().FullName)
                    {
                        return false;
                    }
                }
            }

            return true;
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