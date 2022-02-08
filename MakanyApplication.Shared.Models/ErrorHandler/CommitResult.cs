namespace MakanyApplication.Shared.Models.ErrorHandler
{
    public class CommitResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorCode { get; set; }
        public ErrorType ErrorType { get; set; }
    }
    public class CommitResult<TValue> : CommitResult
    {
        public TValue Value { get; set; }
    }
    public enum ErrorType { None, Error, Info, Warring }
}
