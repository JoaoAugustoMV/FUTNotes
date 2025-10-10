using System.Net.Mail;

namespace FootNotes.Crooscutting.Utils
{
    public class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress addr = new (email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
