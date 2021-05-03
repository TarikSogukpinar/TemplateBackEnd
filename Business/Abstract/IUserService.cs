using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(int id);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        
        IDataResult<List<User>> GetAll(User user);
        IResult UpdateInfo(User user);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}