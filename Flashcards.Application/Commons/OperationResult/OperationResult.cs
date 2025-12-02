namespace Flashcards.Application.Commons.OperationResult
{
    public class OperationResult<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public List<string>? ErrorsMessages { get; set; }

        public static OperationResult<T> Success(T value) =>
            new()
            {
                Data = value,
                IsSuccess = true,
                ErrorMessage = null,
                ErrorsMessages = null
            };

        public static OperationResult<T> Failure(string errorMessage) =>
            new()
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                ErrorsMessages = null
            };

        public static OperationResult<T> Failure(List<string> errorsMessages) =>
            new()
            {
                IsSuccess = false,
                ErrorsMessages = errorsMessages,
                ErrorMessage = string.Join(", ", errorsMessages)
            };
    }
}