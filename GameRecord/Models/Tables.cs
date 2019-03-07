using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.Models
{
    class Tables: BindableBase
    {
        private int table;
        public int Table {
            get { return table; }
            set
            { 
                SetProperty(ref table, value);
                RaisePropertyChanged("Table");
            }
        }
        private int game;
        public int Game {
            get { return game; }
            set
            { 
                SetProperty(ref game, value);
                RaisePropertyChanged("Game");
            }
        }
        private int open;
        public int Open {
            get { return open; }
            set
            { 
                SetProperty(ref open, value);
                RaisePropertyChanged("Open");
            }
        }
    }
}
