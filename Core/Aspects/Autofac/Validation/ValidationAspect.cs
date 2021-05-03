using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;

        private readonly string messages = "Bu bir doğrulama sınıfı değil";

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) throw new Exception(messages);

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator) Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities) ValidationTool.Validate(validator, entity);
        }
    }
}