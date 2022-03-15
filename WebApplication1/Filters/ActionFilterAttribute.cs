using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = context.Result;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            CommonResponse commonResponse = null;
            if (context.Result is ObjectResult objectResult && objectResult.Value != null)
            {
                if (objectResult.StatusCode == 200 || objectResult.StatusCode == 201)
                {
                    commonResponse = new CommonResponse
                    {
                        StatusCode = (int)objectResult.StatusCode,
                        Success = true,
                        Message = "Success",
                        Data = objectResult.Value
                    };
                }
                else if (objectResult.StatusCode == 400 || objectResult.StatusCode == 404)
                {
                    commonResponse = new CommonResponse
                    {
                        StatusCode = (int)objectResult.StatusCode,
                        Success = false,
                        Message = "fail",
                        Data = objectResult.Value
                    };
                }
            }
            else
            {
                commonResponse = new CommonResponse
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "fail",
                    Data = "Internal server error"
                };
            }
            context.Result =new OkObjectResult(commonResponse);
        }

    }
}
