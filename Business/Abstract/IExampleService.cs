using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IExampleService
    {
        IDataResult<List<Example>> GetAll();
        IDataResult<Example> GetById(int exampleId);
        IResult Add(Example example);
        IResult Update(Example example);
        IResult Delete(Example example);
    }
}