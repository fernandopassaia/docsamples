using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natific.Domain.Command
{
    //Here how it Works: I've created a Common Return for the Messages on Post/Put/Delete. So we have a padronized
    //message to show the user. We have 3 fields: Success (true or false), the Message ("Customer saved with success"
    //or "Fix the errors") and a Object: On Object i can put the Saved-Object (in case of success) OR the Validations
    //errors. All My Entities derives from "Notifiable" (Nuget FluentValidator) and Notifiable can handler my validations
    //and store my errors. So in case of error, i will put my "Notifications" (an array of errors) and send back to UI.

    public interface IBaseCommandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }

    public class BaseCommandResult : IBaseCommandResult
    {
        public BaseCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}