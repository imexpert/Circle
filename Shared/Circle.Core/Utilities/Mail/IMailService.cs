namespace Circle.Core.Utilities.Mail
{
    public interface IMailService
    {
        void Send(EmailMessage emailMessage);
    }
}