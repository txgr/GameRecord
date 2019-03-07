using GameRecord.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.ViewModels
{
    class AddUserViewModel : BindableBase, IInteractionRequestAware
    {
        public INotification Notification { get; set ; }

        public Action FinishInteraction { get; set; }

        public DelegateCommand AddCommand { get; private set; }

        public  AddUserViewModel() {
            AddCommand = new DelegateCommand(AddItem);
        }

        private void AddItem()
        {
          //  _notification.SelectedItem = SelectedItem;
         //   _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }
    }
}
