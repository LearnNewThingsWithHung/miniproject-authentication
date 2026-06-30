using FluentValidation;

namespace MiniProjectAuthentication.Service.Auth.Validate;

public class RegisterRequestValidation: AbstractValidator<Request.RegisterRequest>
{
    public RegisterRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email không được để trống.")
            .EmailAddress().WithMessage("Định dạng Email không hợp lệ.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Mật khẩu không được để trống.")
            .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.")
            .Matches(@"[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết hoa.")
            .Matches(@"[a-z]").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết thường.")
            .Matches(@"[0-9]").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
            .Matches(@"[\^$*.\[\]{}()?""!@#%&/\\,><':;|_~`+= -]").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt.");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Vui lòng xác nhận lại mật khẩu.")
            .Equal(x => x.Password).WithMessage("Mật khẩu xác nhận không trùng khớp.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Số điện thoại không được để trống.")
            .Matches(@"^(03|05|07|08|09)\d{8}$")
            .WithMessage("Số điện thoại Việt Nam không hợp lệ (phải bắt đầu bằng 03, 05, 07, 08, 09 và đủ 10 chữ số).");
    }
}