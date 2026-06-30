using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniProjectAuthentication.Repo;
using MiniProjectAuthentication.Repo.Entity;
using MiniProjectAuthentication.Repo.Enum;
using MiniProjectAuthentication.Service.Exceptions;
using MiniProjectAuthentication.Service.MailService;
using ArgumentException = MiniProjectAuthentication.Service.Exceptions.ArgumentException;
using ValidationException = FluentValidation.ValidationException;

namespace MiniProjectAuthentication.Service.Auth;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    private readonly IValidator<Request.RegisterRequest> _registerValidator;
    private readonly MailService.IService _emailService;
    private readonly CacheService.IService _cacheService;
    private readonly JwtService.IService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Service(AppDbContext context, 
        IValidator<Request.RegisterRequest> registerValidator, 
        MailService.IService emailService, CacheService.IService cacheService, 
        JwtService.IService jwtService, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = context;
        _registerValidator = registerValidator;
        _emailService = emailService;
        _cacheService = cacheService;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Register(Request.RegisterRequest request)
    {
        var validationResult = await _registerValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationExceptionBuid("Invalid registration request");
        }

        var isExistEmail = await _dbContext.Users.AnyAsync(x => x.Email == request.Email);
        if (isExistEmail)
        {
            throw new ConflictException("User with this email already exists");
        }
        
        var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, hashType: BCrypt.Net.HashType.SHA256); 
            
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Role = Role.Customer,
            Email = request.Email,
            EmailVerified = false,
            PasswordHash = hashedPassword,
            PhoneNumber = request.PhoneNumber,
            PhoneNumberConfirmed =  false,
            IsLocked = false,
        };
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        try
        {

            Random random = new Random();

            var codeVerify = random.Next(100000, 999999);
            var key = request.Email;
            var value = codeVerify.ToString();

            await _cacheService.Set(key, value, TimeSpan.FromMinutes(5));

            await _emailService.SendMail(new MailContent()
            {
                To = request.Email,
                Subject = "Chào mừng bạn đến với dự án mini của tôi",
                Body = MailTemplate.EmailContainOtp(codeVerify.ToString())
            });

        }
        catch (Exception ex)
        {
            return "Đăng ký thành công. Đang gặp sự cố gửi mail, vui lòng bấm nút Gửi lại mã OT";
        }


        return "Đăng ký thành công";
        
    }

    public async Task<Response.LoginResponse> Login(Request.LoginRequest request)
    {
        var isExistEmail = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (isExistEmail == null)
        {
            throw new BadRequestException("Email không tồn tại trong hệ thống");
        }
        var hashedPassword = BCrypt.Net.BCrypt.EnhancedVerify(
            request.Password, isExistEmail.PasswordHash,
                hashType: BCrypt.Net.HashType.SHA256);
        if (!hashedPassword)
        {
            throw new BadRequestException("Password không khớp");
        }

        var newClaims = new List<Claim>()
        {
            new Claim("UserId", isExistEmail.Id.ToString()),
            new Claim("Email", isExistEmail.Email),
            new Claim("Role", isExistEmail.Role.ToString()),
        };
        
        var accessToken = _jwtService.GenerateAccessToken(newClaims);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var newRefreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId =  isExistEmail.Id,
            TokenHash = refreshToken,
            Revoked = false,
            RevokedAt = null,
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddDays(7),
        };

        _dbContext.Add(newRefreshToken);
        await _dbContext.SaveChangesAsync();

        return new Response.LoginResponse()
        {
            AccescToken = accessToken,
            RefreshToken = refreshToken,
            Message = "Đăng nhập thành công"
        };
    }

    public async Task<string> VerifyEmail(Request.VerifyEmailRequest request)
    {
        var isExistEmail = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (isExistEmail == null)
        {
            throw new NotFoundException("Không tìm thấy email");
        }
        
        var code = await _cacheService.Get<string>(request.Email);
        
        if (code == null)
        {
            throw new NotFoundException("Không tìm thấy code, vui lòng gửi lại");
        }

        if (code != request.Code)
        {
            throw new ConflictException("Code không khớp, vui lòng thử lại");
        }
        
        isExistEmail.EmailVerified = true;
        await _cacheService.Remove(request.Email);
        await _dbContext.SaveChangesAsync();
        return "Xác thực email thành công";
    }
}