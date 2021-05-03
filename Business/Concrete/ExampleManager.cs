using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ExampleManager : IExampleService
    {
        private readonly IExampleDal _exampleDal;

        public ExampleManager(IExampleDal exampleDal)
        {
            _exampleDal = exampleDal;
        }

        public IDataResult<List<Example>> GetAll()
        {
            return new SuccessDataResult<List<Example>>(_exampleDal.GetAll());
        }

        public IDataResult<Example> GetById(int exampleId)
        {
            return new SuccessDataResult<Example>(_exampleDal.Get(c => c.Id == exampleId));
        }
        //Example AOP
        [SecuredOperation("product.add,admin,moderator")]
        [ValidationAspect(typeof(ExampleValidator))]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IResult Add(Example example)
        {
            //Example Check Business Rules
            var result = BusinessRules.RunBusinessRules(CheckExampleNameIfExists(example.Name));
            if (result != null)
            {
                return result;
            }

            _exampleDal.Add(example);
            return new SuccessResult();
        }

        public IResult Update(Example example)
        {
            var result = BusinessRules.RunBusinessRules();
            if (result != null)
            {
                return result;
            }

            _exampleDal.Update(example);
            return new SuccessResult();
        }

        public IResult Delete(Example example)
        {
            var result = BusinessRules.RunBusinessRules();
            if (result != null)
            {
                return result;
            }

            _exampleDal.Delete(example);
            return new SuccessResult();
        }
        
        //Example Business Rules

        private IResult CheckExampleNameIfExists(string exampleName)
        {
            var result = _exampleDal.GetAll(c => c.Name == exampleName).Any();
            if (!result)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}