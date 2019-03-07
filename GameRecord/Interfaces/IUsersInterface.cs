using GameRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.Interfaces
{
    interface IUsersInterface
    {
        List<User> GetAllUsers();
        List<User> GetAllUsersByStatus(int status);
        void SaveAllUsers(List<User> users);

        Tables GetTables();
    }
}
