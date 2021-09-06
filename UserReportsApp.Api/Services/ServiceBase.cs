using SmartFormat;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Services
{
    public abstract class ServiceBase
    {
        protected virtual IServiceActionResult<TObject> Success<TObject>(TObject obj)
            => new SuccessObjectResult<TObject>(obj);
        protected virtual IServiceActionResult Success()
            => new SuccessResult();
        protected virtual IServiceActionResult Error(string errorMessage)
            => new ErrorResult(errorMessage);
        protected virtual IServiceActionResult Error(string errorMessage, params object[] args)
            => new ErrorResult(errorMessage.FormatSmart(args));
    }
}
