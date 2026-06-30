using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MiniProjectAuthentication.Repo;
using MiniProjectAuthentication.Repo.Entity;
using MiniProjectAuthentication.Repo.Enum;
using MiniProjectAuthentication.Service.Exceptions;
using MiniProjectAuthentication.Service.MailService;
using ValidationException = FluentValidation.ValidationException;

namespace MiniProjectAuthentication.Service.Auth;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    private readonly IValidator<Request.RegisterRequest> _registerValidator;
    private readonly MailService.IService _emailService;
    private readonly CacheService.IService _cacheService;

    public Service(AppDbContext context, 
        IValidator<Request.RegisterRequest> registerValidator, 
        MailService.IService emailService, CacheService.IService cacheService)
    {
        _dbContext = context;
        _registerValidator = registerValidator;
        _emailService = emailService;
        _cacheService = cacheService;
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
            var value = codeVerify;

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

    public Task<string> Login(Request.LoginRequest request)
    {
        throw new NotImplementedException();
    }
}