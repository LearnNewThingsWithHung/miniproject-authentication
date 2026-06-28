namespace MiniProjectAuthentication.Service.Auth;

public interface IService
{
    public Task<string> Register(Request.RegisterRequest request);
    public Task<string> Login(Request.LoginRequest request);
}