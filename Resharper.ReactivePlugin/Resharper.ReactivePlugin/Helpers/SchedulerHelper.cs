namespace Resharper.ReactivePlugin.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.ReSharper.Psi;
    using JetBrains.ReSharper.Psi.CSharp.Tree;
    using JetBrains.ReSharper.Psi.Util;
    using IParameter = JetBrains.ReSharper.Psi.IParameter;

    public static class SchedulerHelper
    {
        private const string SchedulerInterfaceName = "System.Reactive.Concurrency.IScheduler";

        public static bool HasSchedulerParameter<T>(T owner, IArgumentList argumentList, out IParameter schedulerParameter) where T : IParametersOwner
        {
            try
            {
                return DoesParametersContainSchedulerInterface(owner.Parameters, out schedulerParameter);
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn);

                schedulerParameter = null;
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

        public static bool HasOverloadWithSchedulerParameter(IMethod method, IInvocationExpression invocationExpression)
        {
            try
            {
                var possibleOverloads = TypeElementUtil.GetAllOverridableMembers(method.GetContainingType())
                                                       .Select(omi => omi.Member as IMethod)
                                                       .Where(om => om.IsExtensionMethod == method.IsExtensionMethod)
                                                       .Where(om => om.ShortName == method.ShortName)
                                                       .Select(om => new PossibleOverload(method, om))
                                                       .ToArray();

                var fitleredOverloads =
                    possibleOverloads.Where(po => !po.OriginalParameters.Equals(po.OverloadParameters))
                                     .Where(po => po.OriginalParameters.Except(po.OverloadParameters).Count() == 0)
                                     .Where(po => po.OverloadParameters.Except(po.OriginalParameters).Count() == 1)
                                     .ToArray();

                var hasOverload =
                    fitleredOverloads.Any(
                        po =>
                        po.OverloadParameters.Except(po.OriginalParameters).First().TypeName == SchedulerInterfaceName);

                return hasOverload;
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
            schedulerParameter = parameters
                .Where(pt => pt.Type is IDeclaredType)
                .SingleOrDefault(pt =>
                                     {
                                         var declaredType = pt.Type.GetScalarType();
                                         return declaredType != null && declaredType.GetClrName().FullName == SchedulerInterfaceName;
                                     });

            return schedulerParameter != null; 
        }
    }
}