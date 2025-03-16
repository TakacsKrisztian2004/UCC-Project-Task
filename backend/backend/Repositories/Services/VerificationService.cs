using backend.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repositories.Interfaces;
using Email_Test_API.Models.Dtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace BackEnd.Repositories.Services
{
    public class VerificationService : IVerificationInterface
    {
        private readonly userreportsContext UserReportsContext;
        private readonly IConfiguration configuration;
        public int code;

        public VerificationService(userreportsContext UserReportsContext, IConfiguration configuration)
        {
            this.UserReportsContext = UserReportsContext;
            this.configuration = configuration;
        }

        public async Task<verification> Post(CreateVerificationDto createVerificationDto)
        {
            Random random = new Random();
            code = random.Next(100000, 999999);

            verification verification = new verification
            {
                Code = code.ToString(),
                Email = createVerificationDto.Email,
            };

            await UserReportsContext.verifications.AddAsync(verification);
            await UserReportsContext.SaveChangesAsync();
            return verification;
        }

        public async Task<bool> SendVerificationEmail(EmailDto request, string code)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailSettings:EmailUserName").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;

                var bodyBuilder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText("EmailTemplate.html"))
                {
                    bodyBuilder.HtmlBody = SourceReader.ReadToEnd();
                }
                bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{code}", code);

                email.Body = bodyBuilder.ToMessageBody();

                using SmtpClient smtp = new SmtpClient();
                smtp.Connect(configuration.GetSection("EmailSettings:EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(configuration.GetSection("EmailSettings:EmailUserName").Value, configuration.GetSection("EmailSettings:EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<verification>> GetAll()
        {
            return await UserReportsContext.verifications.ToListAsync();
        }

        public async Task<verification> GetById(int id)
        {
            return await UserReportsContext.verifications.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<verification> GetByEmail(string email)
        {
            return await UserReportsContext.verifications.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<verification> Put(int id, ModifyVerificationDto modifyVerificationDto)
        {
            verification? existingverification = await UserReportsContext.verifications.FirstOrDefaultAsync(x => x.ID == id);

            if (existingverification != null)
            {
                existingverification.Code = modifyVerificationDto.Code;
                existingverification.Email = modifyVerificationDto.Email;

                UserReportsContext.Update(existingverification);
                await UserReportsContext.SaveChangesAsync();

                return existingverification;
            }

            return null;
        }

        public async Task<bool> VerifyCodeAsync(string code, string email)
        {
            verification? verification = await UserReportsContext.verifications.FirstOrDefaultAsync(v => v.Email == email);

            if (verification != null && verification.Code == code)
            {
                return true;
            }

            return false;
        }

        public async Task<verification> DeleteById(int id)
        {
            verification? verification = await UserReportsContext.verifications.FirstOrDefaultAsync(x => x.ID == id);

            if (verification != null)
            {
                UserReportsContext.verifications.Remove(verification);
                await UserReportsContext.SaveChangesAsync();
            }

            return verification;
        }
    }
}