using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.Models
{
    class Wagers: BindableBase
    {
        private bool banker;

        public bool Banker
        {
            get { return banker; }
            set
            {
                SetProperty(ref banker, value);
                RaisePropertyChanged("Banker");
            }
        }

        private bool player;

        public bool Player
        {
            get { return player; }
            set
            {
                SetProperty(ref player, value);
                RaisePropertyChanged("Player");
            }
        }
        private bool tie;

        public bool Tie
        {
            get { return tie; }
            set
            {
                SetProperty(ref tie, value);
                RaisePropertyChanged("Tie");
            }
        }
        private bool bankerPairs;

        public bool BankerPairs
        {
            get { return bankerPairs; }
            set
            {
                SetProperty(ref bankerPairs, value);
                RaisePropertyChanged("BankerPairs");
            }
        }
        private bool playerPairs;

        public bool PlayerPairs
        {
            get { return playerPairs; }
            set
            {
                SetProperty(ref playerPairs, value);
                RaisePropertyChanged("PlayerPairs");
            }
        }
    }
}
