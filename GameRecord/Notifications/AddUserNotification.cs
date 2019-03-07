using GameRecord.Models;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.Notifications
{
    class AddUserNotification : Confirmation, IAddUserNotification
    {
        public User UserItem { get ; set ; }
    }
}
