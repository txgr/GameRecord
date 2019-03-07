using GameRecord.Models;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.Notifications
{
    interface IAddUserNotification: IConfirmation
    {
        User UserItem { get; set; }
    }
}
