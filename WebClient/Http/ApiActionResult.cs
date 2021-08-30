using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient
{
    public class ApiActionResult
    {
        //public ApiActionResult(bool success, string errorMessage)
        //{
        //    Success = success;
        //    ErrorMessage = errorMessage;
        //}

        //public ApiActionResult(string errorMessage)
        //{
        //    ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        //}

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class EntityApiActionResult<TEntity> : ApiActionResult
    {
        public TEntity Entity { get; set; }

        //public static EntityApiActionResult<TEntity> FromError(string errorMessage)
        //{
        //    return new EntityApiActionResult<TEntity>
        //    {
        //        Success = false,
        //        ErrorMessage = errorMessage
        //    };
        //}

        //public static EntityApiActionResult<TEntity> FromSuccess(TEntity entity)
        //{
        //    return new EntityApiActionResult<TEntity>
        //    {
        //        Success = true,
        //        Entity = entity
        //    };
        //}
    }
}
