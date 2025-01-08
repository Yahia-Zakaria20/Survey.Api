using Microsoft.AspNetCore.Mvc;

namespace Survey.Basket.Api.Errors
{
    public static class Helper
    {

        public static ObjectResult ToProblem(this BaseResult result) 
        {

          var Problem = Results.Problem(statusCode:result.Error.StatusCode);

         var problemdetails = Problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(Problem) as ProblemDetails;

             problemdetails!.Extensions = new Dictionary<string, object?>()
            {
                {"Error" , result.Error }
            };


            return new ObjectResult(problemdetails);
        }
    }
}
