namespace JobCandidateHub.Service.Models;
public class Response
{
    public bool IsSuccess { get; set; } = false;
    public int Code { get; set; }
    public string? Message { get; set; } = string.Empty;
    public object? Data { get; set; }

    public static Response SetResponse(string? message = "Oops somethings went wrong", bool isSuccess = false, object? data = null)
    {
        return new Response
        {
            Message = message,
            IsSuccess = isSuccess,
            Data = data
        };
    }
}
