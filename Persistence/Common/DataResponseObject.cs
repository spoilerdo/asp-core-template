namespace Back_End.Persistence.Common
{
    public class DataResponseObject<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        /// <summary>
        /// Primarily used for empty responses that still need to indicate success.
        /// </summary>
        /// <param name="success"></param>
        public DataResponseObject(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Used when errors occurred within the operation.
        /// </summary>
        /// <param name="message">The errormessage</param>
        public DataResponseObject(string message)
        {
            Success = false;
            Message = message;
        }

        /// <summary>
        /// Used when operation was completed successfully and you wish to return data.
        /// </summary>
        /// <param name="data">The data to return</param>
        public DataResponseObject(T data)
        {
            Success = true;
            Data = data;
        }
    }
}