namespace Dinex.Infra
{
    public class ValidationMiddleware : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
