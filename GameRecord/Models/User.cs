using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRecord.Models
{
    class User: BindableBase
    {

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                SetProperty(ref number, value);
                RaisePropertyChanged("Number");
            }
        } //编号
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
                RaisePropertyChanged("Name");
            }
        } //姓名
        public double InitScore { get; set; } //初始分
        private double totalScore;
        public double TotalScore
        {
            get { return totalScore; }
            set
            {
                SetProperty(ref totalScore, value);
                RaisePropertyChanged("TotalScore");
            }
        } //总积分
        public double Commission { get; set; } //佣金
        private double surplusScore;
        public double SurplusScore
        {
            get { return surplusScore; }
            set
            {
                SetProperty(ref surplusScore, value);
                RaisePropertyChanged("SurplusScore");
            }
        } //剩余分
        public int Weight { get; set; } //排序
        public int Status { get; set; } //状态
        public int Banker { get; set; } //庄
        public int Player { get; set; } //闲
        public int Tie { get; set; } //和
        public int BankerPairs { get; set; } //庄对
        public int PlayerPairs { get; set; } //闲对
        public int Table { get; set; } //桌
        
        public bool IsCheck { get; set; }
        public int Game { get; set; }//局

        public double TmpScore { get; set; } //临时积分

        private double currentScore; //本局得分

        public double CurrentScore
        {
            get { return currentScore; }
            set
            {
                SetProperty(ref currentScore, value);
                RaisePropertyChanged("CurrentScore");
            }
        }

        private int total;
        public int Total//合计
        {
            get { return total; }
            set
            {
              //  total = Tie + BankerPairs + Banker+ PlayerPairs+ Player;
                SetProperty(ref total, value);
                RaisePropertyChanged("Total");
            }
        }
    }
}
