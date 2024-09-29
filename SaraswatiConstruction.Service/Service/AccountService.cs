using SaraswatiConstruction.Domain.Constant;
using SaraswatiConstruction.Domain.GlobalResource;
using SaraswatiConstruction.Domain.Models;
using SaraswatiConstruction.Infrastructure.IRepository;
using SaraswatiConstruction.Service.IService;
using SaraswatiConstruction.Utility;
using SaraswatiConstruction.Utility.CommunicationService;
using System.Text;
using System.Web;

namespace SaraswatiConstruction.Service.Service
{
    public class AccountService(IAccountRepository _accountRepository, IEmailService _emailService) : IAccountService
    {
        public async Task<Result> RegisterUser(UserDetail userDetail)
        {
            try
            {
                Result result = await _accountRepository.RegisterUser(userDetail);

                if (result.ResultCode == Convert.ToInt32(CommonConstants.Zero))
                {

                    string token = CommonFunctions.GenerateToken(result.Id);
                    string encodedToken = HttpUtility.UrlEncode(token);
                    StringBuilder mailBody = new StringBuilder();
                    mailBody.Append("<h1>Welcome to Saraswati Construction</h1>");
                    mailBody.Append($"<p>Dear {userDetail.FirstName} {userDetail.LastName},</p>");
                    mailBody.Append("<p>Please verify your email address now. Making sure we have your current email address helps us keep your account safe and secure.</p>");
                    mailBody.Append("<br />");
                    mailBody.Append($"<p><a href='{userDetail.Url}{encodedToken}' style='background-color: #1E90FF; color: #ffffff; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer;'>Verify email</a></p>");
                    mailBody.Append("<br />");
                    mailBody.Append("<p>We'll always let you know when there is any activity on your account. This helps keep your account safe.</p>");
                    mailBody.Append("<p>If you didn't make this request, please contact us.</p>");
                    mailBody.Append("<br />");
                    mailBody.Append("<p>Thanks,</p>");
                    mailBody.Append("<p>Saraswati Construction</p>");


                    string subject = "Email Verification - Saraswati Construction";

                    if (!_emailService.SendEmail(userDetail.Email, mailBody, subject))
                    {
                        result.ResultCode = Convert.ToInt32(CommonConstants.Four);
                        result.ResultDescription = Messages.SomethingWrongtoSendMail;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Result> VerifyEmail(string? token)
        {
            Result result = new Result();
            try
            {
                var decryptedToken = CommonFunctions.Decrypt(token);

                // Extract expiration time from the decrypted token
                var parts = decryptedToken.Split(':');
                if (parts.Length != 5)
                {
                    result.ResultCode = Convert.ToInt32(CommonConstants.Two);
                    result.ResultDescription = Messages.CorruptedToken;
                    return result;
                }

                var expirationDate = DateTime.Parse($"{parts[2]}:{parts[3]}:{parts[4]}");

                // Check if the token is expired
                if (expirationDate >= DateTime.UtcNow)
                {
                    result = await _accountRepository.VerifyEmail(Convert.ToInt32(parts[0]));
                    if (result.ResultCode == Convert.ToInt32(CommonConstants.Zero))
                    {
                        result.Id = CommonFunctions.Encrypt(parts[0]);
                    }
                }
                else
                {
                    result.ResultCode = Convert.ToInt32(CommonConstants.One);
                    result.ResultDescription = Messages.TokenExpired;
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<UserDetail> Login(UserDetail userCredential)
        {
            UserDetail userDetail = new UserDetail();
            try
            {
                userDetail = await _accountRepository.Login(userCredential);
                if (userDetail.Password != userCredential.Password)
                {
                    userDetail.ResultCode = CommonConstants.Two;
                    userDetail.ResultDescription = "Invalid credential";

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return userDetail;
        }


    }
}
