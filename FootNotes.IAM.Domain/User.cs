using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FootNotes.Core.Domain;
using FootNotes.Crooscutting.Utils;

namespace FootNotes.IAM.Domain
{    
    public class User : Entity, IAggregateRoot
    {
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserType UserType { get; set; }

        public User(string username, string email, string passwordHash, UserType userType)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            UserType = userType;

            ThrowIfInvalid();
        }

        public override void ThrowIfInvalid()
        {
            StringBuilder errorMsg = new();

            if (string.IsNullOrWhiteSpace(Username))
                errorMsg.AppendLine("Username is required;");
            if (string.IsNullOrWhiteSpace(Email))
                errorMsg.AppendLine("Email is required;");
            if (!ValidationHelper.IsValidEmail(Email))
                errorMsg.AppendLine("Email is invalid;");
            if (string.IsNullOrWhiteSpace(PasswordHash))
                errorMsg.AppendLine("PasswordHash is required;");
            if (!Enum.IsDefined(typeof(UserType), UserType))
                errorMsg.AppendLine("Type is invalid;");

            string msg = errorMsg.ToString();

            if(!string.IsNullOrWhiteSpace(msg))
                throw new EntityInvalidException(msg);
            
        }


    }
}
