namespace MiniProjectAuthentication.Service.Auth;

public interface IService
{
    public Task<string> Register(Request.RegisterRequest request);
    public Task<Response.LoginResponse> Login(Request.LoginRequest request);
    public Task<string> VerifyEmail(Request.VerifyEmailRequest request);
}