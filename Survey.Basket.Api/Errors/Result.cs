using Survey.Basket.Api.Error;

namespace Survey.Basket.Api.Errors
{
    public class Result:BaseResult
    {

        public Result(bool issuccess, ApiResponse error) :base(issuccess , error)
        {
            
        }


        public static Result Success() 
        {
            return new Result(true, ApiResponse.None);
        }

        public static Result Falier(ApiResponse error)
        {
            return new Result(false, error);
        }
    }



    public class Result<Tvalue> : BaseResult
    {
        private readonly Tvalue _value;

        public Tvalue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Failure results cannot have value");  //To Validate He Send A Value Or Not

        public Result(Tvalue tvalue , bool Issuccess, ApiResponse error) : base(Issuccess, error)
        {
            _value = tvalue;
        }


        public static Result<Tvalue> Success(Tvalue value)
        {
            return new Result<Tvalue>(value,true,ApiResponse.None);    
        }

        public static Result<Tvalue> Falier(ApiResponse error)
        {
            return new Result<Tvalue>(default,false, error);
        }
    }
}
