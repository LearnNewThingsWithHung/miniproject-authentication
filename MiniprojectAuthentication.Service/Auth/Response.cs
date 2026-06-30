namespace MiniProjectAuthentication.Service.Auth;

public class Response
{
    public class LoginResponse
    {
        public string AccescToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
    }
}