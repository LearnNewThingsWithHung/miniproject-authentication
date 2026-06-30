namespace MiniProjectAuthentication.Service.MailService;

public static class MailTemplate
{
    public static string EmailContainOtp(string otpCode)
    {
        var htmlBody = """
        <!DOCTYPE html>
        <html lang="vi">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Mã xác thực OTP - Phan Tan Hung</title>
            <style>
                @media screen and (max-width: 600px) {
                    .email-container { width: 100% !important; padding: 15px !important; }
                    .content-wrapper { padding: 30px 20px !important; }
                    .otp-box { font-size: 28px !important; letter-spacing: 6px !important; padding: 12px !important; }
                }
            </style>
        </head>
        <body style="margin: 0; padding: 0; background-color: #eef3f0; font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif; color: #3f3f3f; -webkit-font-smoothing: antialiased;">
        
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: #eef3f0; padding: 50px 0;">
                <tr>
                    <td align="center">
                        
                        <table class="email-container" border="0" cellpadding="0" cellspacing="0" width="580" style="background-color: #ffffff; border-radius: 12px; overflow: hidden; border: 1px solid #d6e2dc; box-shadow: 0 10px 25px -5px rgba(81, 108, 108, 0.05), 0 8px 10px -6px rgba(81, 108, 108, 0.05);">
                            
                            <tr>
                                <td align="center" style="padding: 40px 40px 10px 40px;">
                                    <h1 style="margin: 0; color: #7a9c59; font-size: 22px; font-weight: 640; letter-spacing: 2px; text-transform: uppercase;">
                                        Phan Tan Hung
                                    </h1>
                                    <div style="height: 2px; width: 30px; background-color: #7a9c59; margin: 15px auto 0 auto; border-radius: 2px;"></div>
                                </td>
                            </tr>
        
                            <tr>
                                <td class="content-wrapper" style="padding: 30px 45px 40px 45px;">
                                    <p style="margin-top: 0; margin-bottom: 16px; color: #516c6c; font-size: 18px; font-weight: 600; text-align: center;">
                                        Mã xác thực OTP của bạn
                                    </p>
                                    
                                    <p style="margin: 0 0 24px 0; font-size: 15px; line-height: 1.6; color: #516c6c; text-align: center;">
                                        Chào bạn, bạn đang thực hiện hành động xác thực tại hệ thống <strong>Phan Tan Hung</strong>. Vui lòng sử dụng mã OTP dưới đây để hoàn tất quá trình:
                                    </p>
        
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin: 24px 0;">
                                        <tr>
                                            <td align="center">
                                                <div class="otp-box" style="background-color: #f4f8f6; border: 2px dashed #7a9c59; color: #2f4646; font-family: 'Courier New', Courier, monospace; font-size: 36px; font-weight: 700; letter-spacing: 10px; padding: 16px 40px; border-radius: 8px; display: inline-block;">
                                                    {{otp_code}}
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
        
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: #fbf4f4; border-radius: 8px; border-left: 4px solid #d9534f; margin-top: 30px;">
                                        <tr>
                                            <td style="padding: 16px 20px;">
                                                <p style="margin: 0 0 4px 0; font-size: 13px; font-weight: 600; color: #b94a48;">
                                                    Lưu ý bảo mật quan trọng:
                                                </p>
                                                <p style="margin: 0; font-size: 12px; color: #b94a48; line-height: 1.5;">
                                                    Mã OTP này có hiệu lực trong vòng <strong>5 phút</strong> và chỉ sử dụng được 1 lần duy nhất. Tuyệt đối không chia sẻ mã này cho bất kỳ ai (kể cả nhân viên hỗ trợ của chúng tôi).
                                                </p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
        
                            <tr>
                                <td style="background-color: #fafbfa; padding: 30px 45px; text-align: center; border-top: 1px solid #d6e2dc;">
                                    <p style="margin: 0 0 12px 0; font-size: 12px; color: #6b8079; line-height: 1.5;">
                                        Bạn nhận được email này vì hệ thống yêu cầu mã xác thực giao dịch hoặc đăng nhập.<br>Nếu bạn không thực hiện yêu cầu này, vui lòng đổi mật khẩu ngay lập tức.
                                    </p>
                                    <p style="margin: 0; font-size: 11px; color: #bcd0c7; letter-spacing: 0.5px;">
                                        &copy; 2026 Phan Tan Hung. All rights reserved.
                                    </p>
                                </td>
                            </tr>
        
                        </table>
                    </td>
                </tr>
            </table>
        
        </body>
        </html>
        """;

        return htmlBody.Replace("{{otp_code}}", otpCode);
    }
}