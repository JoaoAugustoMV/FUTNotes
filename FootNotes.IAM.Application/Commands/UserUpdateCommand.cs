using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.Crooscutting.Utils;
using FootNotes.IAM.Domain;

namespace FootNotes.IAM.Application.Commands
{
    public class UserUpdateCommand : Command
    {
        public string Username { get; set; }        
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }



        public override bool IsValid(out string msg)
        {
            StringBuilder errorMsg = new();

            if (string.IsNullOrWhiteSpace(Username))
                errorMsg.AppendLine("Username is required;");            
            if (string.IsNullOrWhiteSpace(Password))
                errorMsg.AppendLine("PasswordHash is required;");
            if (!Enum.IsDefined(typeof(UserType), UserType))
                errorMsg.AppendLine("Type is invalid;");

            msg = errorMsg.ToString();

            return string.IsNullOrWhiteSpace(msg);
        }
    }
}
