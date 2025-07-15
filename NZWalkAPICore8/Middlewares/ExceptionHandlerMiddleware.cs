namespace NZWalkAPICore8.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public ILogger<ExceptionHandlerMiddleware> logger { get; }
        public RequestDelegate next { get; }

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next) 
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext )
        {
            try
            {
                await this.next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString();
                //Log the exception
                logger.LogError(ex, $"{errorId} : {ex.Message}" );



                //Return custom Error Response
                //httpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    ErrorId = errorId,
                    Message = " Some thing wrong we are looking this !"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }

        }
    }
}
