using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace AlkemyChallenge.Helper
{
    public class ErrorHelper
    {
        public static List<ErrorModel> GetModelStateErrors(ModelStateDictionary modelState)
        {
            return modelState.Select(x => new ErrorModel()
            {
                Key = x.Key,
                Message = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
            }
            ).ToList();
            
        }
    }

    public class ErrorModel
    {
        public string Key { get; set; }
        public List<string> Message { get; set; }
    }
}
