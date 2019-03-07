using GameRecord.Interfaces;
using GameRecord.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameRecord.Services
{
    class UsersService : IUsersInterface
    {
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            string usersFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSource\Users.json");
            try { 
            StreamReader file = File.OpenText(usersFileName);
            JsonTextReader reader = new JsonTextReader(file);
            JArray jsonObject = (JArray)JToken.ReadFrom(reader);
            for (int i = 0; i < jsonObject.Count; i++)
            {
                User user = new User();
                user.Name = jsonObject[i]["Name"].ToString();
                user.Number = int.Parse(jsonObject[i]["Number"].ToString());
                user.InitScore = double.Parse(jsonObject[i]["InitScore"].ToString());
                user.TotalScore = double.Parse(jsonObject[i]["TotalScore"].ToString());
                user.Commission = double.Parse(jsonObject[i]["Commission"].ToString());
                user.Weight = int.Parse(jsonObject[i]["Weight"].ToString());
                user.Status = int.Parse(jsonObject[i]["Status"].ToString());
                user.SurplusScore = double.Parse(jsonObject[i]["SurplusScore"].ToString());
                user.Total = int.Parse(jsonObject[i]["Total"].ToString());
                userList.Add(user);
            }
            file.Close();
            reader.Close();
        } catch {

            };
            return userList;

        }

        public List<User> GetAllUsersByStatus(int status)
        {
            List<User> userList = new List<User>();
            return userList;
        }

        public Tables GetTables()
        {
            Tables tables = new Tables();

            string usersFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSource\Table.json");

            try
            {
                StreamReader file = File.OpenText(usersFileName);
                JsonTextReader reader = new JsonTextReader(file);
                JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                tables.Table = int.Parse(jsonObject["Table"].ToString());
                tables.Game = int.Parse(jsonObject["Game"].ToString());
                tables.Open = int.Parse(jsonObject["Open"].ToString());

                file.Close();
                reader.Close();
             } catch {

            };
            return tables;
        }

        public void SaveAllUsers(List<User> users)
        {
            throw new NotImplementedException();
        }
    }
}
