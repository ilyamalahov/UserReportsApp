using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserReportsApp.Shared.Models
{
    public class SuccessResult : IServiceActionResult
    {
        public bool Success => true;
        public string ErrorMessage => null;
    }

    public class ErrorResult : IServiceActionResult
    {
        public ErrorResult(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }

        public bool Success => false;
        public string ErrorMessage { get; }
    }

    public class SuccessObjectResult<TObject> : SuccessResult, IServiceActionResult<TObject>
    {
        public SuccessObjectResult(TObject obj)
        {
            Object = obj;
        }

        public TObject Object { get; }
    }

    public interface IServiceActionResult<TObject> : IServiceActionResult
    {
        public TObject Object { get; }
    }

    public interface IServiceActionResult
    {
        public bool Success { get; }
        public string ErrorMessage { get; }
    }
}
