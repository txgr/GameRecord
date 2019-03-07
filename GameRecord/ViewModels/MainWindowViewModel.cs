using GameRecord.Models;
using GameRecord.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Interactivity.InteractionRequest;
using GameRecord.Notifications;

namespace GameRecord.ViewModels
{
    class MainWindowViewModel: BindableBase
    {
        private string gameText;

        public string CurrentGameText
        {
            get { return gameText; }
            set
            {
                SetProperty(ref gameText, value);
                RaisePropertyChanged("CurrentGameText");
            }
        }
         ~MainWindowViewModel() {
            SaveUsers("33");
        }


        private string checkText;

        public string CheckText
        {
            get { return checkText; }
            set
            {
                SetProperty(ref checkText, value);
                RaisePropertyChanged("CheckText");
            }
        }
        private string countText;

        //统计
        public string CountText
        {
            get { return countText; }
            set
            {
                SetProperty(ref countText, value);
                RaisePropertyChanged("CountText");
            }
        }
        private Tables tables;

        public Tables CurrentTables
        {   
            get { return tables; }
            set
            {
                SetProperty(ref tables, value);
                RaisePropertyChanged("CurrentTables");
               
            }
        }

        //开奖结果
        private Wagers prizeResult;

        public Wagers PrizeResult
        {
            get { return prizeResult; }
            set {
                SetProperty(ref prizeResult, value);
                RaisePropertyChanged("PrizeResult");
            }
        }

        ObservableCollection<User> userList = new ObservableCollection<User>();
        public ObservableCollection<User> UsersList
        {
            get { return userList; }
            set
            {
                userList = value;
                RaisePropertyChanged("UsersList");
            }
        }

        private void LoadUsers()
        {
            UsersService us = new UsersService();
            var userList = us.GetAllUsers();
            UsersList = new ObservableCollection<User>();
            foreach (var d in userList)
            {
                UsersList.Add(d);
            }

            CurrentTables = us.GetTables();
        }
    

        //   
        public DelegateCommand<string> SaveWagersCommand { get; set; }

        public DelegateCommand<object[]> SelectUserItemCommand { get; set; }
        public DelegateCommand<object[]> EditWagersItemCommand { get; set; }

        public DelegateCommand AddUserNotificationCommand { get; set; }
        public InteractionRequest<IAddUserNotification> AddUserNotificationRequest { get; set; }
        public DelegateCommand<string> SaveUserCommand { get; set; }
        public DelegateCommand<object[]> DeleteUserCommand { get; set; }
        public DelegateCommand<string> NextTableCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand OpenPrizeCommand { get; set; }
        public DelegateCommand CheckCommand { get; set; }

        public DelegateCommand MergePrizeCommand { get; set; }

        //开奖
        private void OpenPrize() {

            foreach (var d in UsersList)
            {
                double score = 0;
               // double commission = 0;
                double tmpScore = 0;
                if (PrizeResult.Player) //闲
                {
                    score = d.Player - d.Banker - d.Tie;
                    tmpScore = d.Player;
                }
                else if (PrizeResult.Banker)//庄
                {
                    score = d.Banker * 0.95 - d.Player - d.Tie;
                    tmpScore = d.Banker;
                }
                else if (PrizeResult.Tie)
                {//和
                    score = d.Tie * 8;
                    tmpScore = d.Tie;
                }

                //闲对
                if (PrizeResult.PlayerPairs)
                {
                    score += d.PlayerPairs * 11;
                    tmpScore += d.PlayerPairs;
                }
                else {
                    score -= d.PlayerPairs;
                }
                //庄对
                if (PrizeResult.BankerPairs)
                {
                    score += d.BankerPairs * 11;
                    tmpScore += d.BankerPairs;
                }
                else {
                    score -= d.BankerPairs;
                }
                d.CurrentScore = score;
              //  d.SurplusScore += score;

              //  d.SurplusScore -= tmpScore;
             //   UsersList.Add(d);
            }

            CheckText = CurrentGameText + "\n已开奖";
        }

        //合并
        private void MergePrize() {
            foreach (var d in UsersList)
            {
                d.SurplusScore += d.CurrentScore;
            }

            CheckText = CurrentGameText + "\n已合并到剩余分，\n请勿重复点击";
        }
        //检查
        private void Check() {
           
            CheckText = null;
            CountText = "统计结果：";
            double Banker = 0;
            double BankerPairs = 0;
            double Player = 0;
            double PlayerPairs = 0;
            double Tie = 0;
            double Total = 0;
            foreach (var d in UsersList)
            {
                Banker += d.Banker;
                BankerPairs += d.BankerPairs;
                Player += d.Player;
                PlayerPairs += d.PlayerPairs;
                Tie += d.Tie;
                
                int t = d.Banker + d.BankerPairs + d.Player + d.PlayerPairs + d.Tie;
                Total += t;
                if (t > d.SurplusScore) {
                    CheckText += d.Name + " 编号:" + d.Number.ToString() + " 剩余分不够\n";
                 //   MessageBox.Show(d.Name+ " 编号:"+d.Number.ToString()+ "余分不够");
                }
            }
            if (CheckText == null) {
                CheckText = "检查通过，可以开奖";
            }
            CountText += " 庄：" + Banker.ToString();
            CountText += "; 闲：" + Player.ToString();
            CountText += "; 庄对：" + BankerPairs.ToString();
            CountText += "; 闲对：" + PlayerPairs.ToString();
            CountText += "; 和：" + Tie.ToString();
            CountText += "; 合计：" + Total.ToString();
            CountText += "; (庄-闲)：" + (Banker - Player).ToString();
        }
   
        //下一局
        public void NextTable(string str) {
            
            //if (CurrentTables.Open == 1) {
            //    MessageBox.Show("当前局还未开奖，如需开始下一局请直接修改");
            //    return;
            //}
            CurrentTables.Table += 1;
            string json = JsonConvert.SerializeObject(CurrentTables);
            try
            {
                File.WriteAllText(@"DataSource\Table.json", json);
                if (CurrentTables != null)
                {
                    CurrentGameText = "第" + CurrentTables.Game.ToString() + "靴第" + CurrentTables.Table.ToString() + "局";
                }
                else
                {
                    CurrentGameText = "第1靴第1局";
                }

                foreach (var d in UsersList)
                {
                    d.Tie = 0;
                    d.Banker = 0;
                    d.BankerPairs = 0;
                    d.Player = 0;
                    d.PlayerPairs = 0;
                    d.CurrentScore = 0;
                }
                //   MessageBox.Show("保存成功");
            }
            catch
            {
                MessageBox.Show("异常，请重试");

            }
        }
        //保存会员
        public void SaveUsers(string str = null) {
            string json = JsonConvert.SerializeObject(UsersList);
       
            try
            {
                File.WriteAllText(@"DataSource\Users.json", json);
                MessageBox.Show("保存成功");
            }
            catch  {
                MessageBox.Show("异常，请重试");

            }
        }

        //删除
        private void DeleteUser(object[] selectedItems) {

            for (int i = 0; i < UsersList.Count; i++) {
                if (UsersList[i].IsCheck)
                {
                    UsersList.Remove(UsersList[i]);
                }
            } 
        }

        private void Refresh() {
            loadData();
        }
        private void EditWagersItemExecute(object[] selectedItems)
        {
            User u = new User();
            try
            {
                u = (User)selectedItems.FirstOrDefault();
              //  MessageBox.Show(u.Name);
            }
            catch (Exception)
            {

                MessageBox.Show("ee");
            }
          //  u = (User)selectedItems.LastOrDefault();
          //  MessageBox.Show(u.Name);
            if (selectedItems != null && selectedItems.Count() > 0)
            {
         //       MessageBox.Show("编辑");
                //User u = new User();
                //u = (User)selectedItems.FirstOrDefault();
                //SelectText = u.Name;

                //foreach (var d in Mylist)
                //{
                //    if (d.Number == 3333)
                //    {
                //        d.Name = "米亚1111";
                //    }

                //    //   WagersList.Add(wager);
                //}
            }
        }

        private void SelectUserItemExecute(object[] selectedItems)
        {
            try {
                if (selectedItems != null && selectedItems.Count() > 0)
                {
                    //int len = selectedItems.Count();
                    //for (int i = 0; i < len; i++) {
                    //    User u = new User();
                    //    u = (User)selectedItems.FirstOrDefault();
                    //}
                    //  List<User> u = new List<User>();
                    //  u = selectedItems;
                    //    User u = new User();
                    //    u = (User)selectedItems.FirstOrDefault();
                    //SelectText = u.Name;

                    //foreach (var d in Mylist)
                    //{
                    //    if (d.Number == 3333) {
                    //        d.Name = "米亚1111";
                    //    }

                    // //   WagersList.Add(wager);
                    //}
                }
            } catch {
              //  MessageBox.Show("error");
            }
            
        }
        
        

        private void loadData() {
            LoadUsers();
       
            //初始开奖结果
            Wagers wa = new Wagers();
            wa.BankerPairs = false;
            wa.Banker = false;
            wa.Player = true;
            wa.PlayerPairs = false;
            wa.Tie = false;
            PrizeResult = wa;

            if (CurrentTables != null)
            {
                CurrentGameText = "第" + CurrentTables.Game.ToString() + "靴第" + CurrentTables.Table.ToString() + "局";
            }
            else
            {
                CurrentGameText = "第1靴第1局";
            }
        }
        public MainWindowViewModel() {
            loadData();

            SaveUserCommand = new DelegateCommand<string>(SaveUsers);
       //     SaveWagersCommand = new DelegateCommand<string>(SaveWagers);
        //    EditWagersItemCommand = new DelegateCommand<object[]>(EditWagersItemExecute);
            SelectUserItemCommand = new DelegateCommand<object[]>(SelectUserItemExecute);
            DeleteUserCommand = new DelegateCommand<object[]>(DeleteUser);
            AddUserNotificationCommand = new DelegateCommand(RaiseAddUserInteraction);
            AddUserNotificationRequest = new InteractionRequest<IAddUserNotification>();
            NextTableCommand = new DelegateCommand<string>(NextTable);
            RefreshCommand = new DelegateCommand(Refresh);
            OpenPrizeCommand = new DelegateCommand(OpenPrize);
            CheckCommand = new DelegateCommand(Check);
            MergePrizeCommand = new DelegateCommand(MergePrize);
        }

        private void RaiseAddUserInteraction()
        {
            AddUserNotificationRequest.Raise(new AddUserNotification { Title = "添加会员" }, r =>
            {
                //if (r.Confirmed && r.SelectedItem != null)
                //    Title = $"User selected: { r.SelectedItem}";
                //else
                //    Title = "User cancelled or didn't select an item";
            });
        }
    }
}
