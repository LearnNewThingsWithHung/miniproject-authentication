namespace MiniProjectAuthentication.Service.Exceptions;

public class ValidationExceptionBuid : AppException
{
    public ValidationExceptionBuid(string detail)
        : base("Bad Request", 400, "VALIDATION_ERROR", detail) { }
}