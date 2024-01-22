namespace BrewTask.Models.GenericResponse
{
    public class ResponseModel
    {
        public object Data { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool Success { get; set; }
        public List<ErrorModel> Errors { get; set; }
        public string ResponseExecutionTime { get; set; }

        public ResponseModel()
        {
            Data = null;
            ResponseCode = ApiResponseCode.Default;
            ResponseMessage = string.Empty;
            Errors = new List<ErrorModel>();
            Success = false;
        }

        public ResponseModel(bool isSuccess)
        {
            if (isSuccess)
                SuccessModel();
            else
                FailedModel();
        }

        public void SuccessModel()
        {
            Data = null;
            ResponseCode = ApiResponseCode.Success;
            ResponseMessage = string.Empty;
            Errors = new List<ErrorModel>();
            Success = true;
        }

        public void FailedModel()
        {
            Data = null;
            ResponseCode = ApiResponseCode.Fail;
            ResponseMessage = string.Empty;
            Errors = new List<ErrorModel>();
            Success = false;
        }
        public void ExceptionModel()
        {
            Data = null;
            ResponseCode = ApiResponseCode.Exception;
            ResponseMessage = string.Empty;
            Errors = new List<ErrorModel>();
            Success = false;
        }
        public void ExceptionModel(Exception exception)
        {
            ExceptionModel();
            if (exception != null)
            {
                Data = exception.StackTrace;
                ResponseMessage = exception.InnerException?.ToString();
            }
        }
        public void ValidationModel()
        {
            Data = null;
            ResponseCode = ApiResponseCode.Validation;
            ResponseMessage = string.Empty;
            Errors = new List<ErrorModel>();
            Success = false;
        }
        public void DefaultModel()
        {
            Data = null;
            ResponseCode = ApiResponseCode.Default;
            ResponseMessage = string.Empty;
            Errors = new List<ErrorModel>();
            Success = false;
        }

        public void AddError(string Error)
        {
            Errors.Add(new ErrorModel()
            {
                ErrorCode = (Errors.Count + 1).ToString(),
                ErrorMessage = Error
            });
        }
    }

    public class ErrorModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorModel(string ErrorCode, string ErrorMessage)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
        }
        public ErrorModel()
        {
        }
    }
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string? LastPageURL { get; set; }
    }
}
