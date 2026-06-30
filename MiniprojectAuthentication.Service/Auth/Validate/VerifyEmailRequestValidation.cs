using FluentValidation;

namespace MiniProjectAuthentication.Service.Auth.Validate;

public class VerifyEmailRequestValidation: AbstractValidator<Request.VerifyEmailRequest>
{
    public VerifyEmailRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email không được để trống.")
            .EmailAddress().WithMessage("Định dạng Email không hợp lệ.");
        
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Mã xác thực không được để trống.")
            .Matches(@"^\d{6}$")
            .WithMessage("Mã xác thực phải gồm đúng 6 chữ số.");
    }
}