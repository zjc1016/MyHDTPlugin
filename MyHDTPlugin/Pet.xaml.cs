using HearthDb.Enums;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace MyHDTPlugin
{


    public partial class MyPet
    {
        private static readonly Random _random = new Random();
        public string nowplaying = "main";
        public ActivePlayer turn = ActivePlayer.None;
        public bool IsDaji = true;
       

        public MyPet()
        {

            InitializeComponent();
            string dllDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string imagePath = System.IO.Path.Combine(dllDir, "bg.jpg");
            MediaBackground.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            PlayVideoLoop("main");
        }
        public void Update(string text, string video)
        {   
            Text.Text = text;
            this.Visibility = Visibility.Visible;
            if (video != null && (nowplaying == "main" || nowplaying == "待机"))
            {
                nowplaying = video;
                PlayVideoLoop(nowplaying);
            }
            if(video == null)
            {
                DaiJi(turn);
            }
            
            

        }
        public void UpdatePosition()
        {
            Canvas.SetBottom(this, Core.OverlayWindow.Height * 15 / 100);
            Canvas.SetLeft(this, Core.OverlayWindow.Width * 15 / 100);
            double width = Core.OverlayWindow.Width * 0.16; // 占窗口 16%
            double height = width * 9 / 16.0; // 保证 16:9
            MyMedia.Width = width;
            MyMedia.Height = height;
            MediaBackground.Width = width;
            MediaBackground.Height = height;

        }
        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            this.Visibility = Visibility.Hidden;
        }


        public void CheckAttack(List<Entity> opp, List<Entity> player, ActivePlayer Turn)
        {
            
            int OppTotalAttack = opp
                    .Where(e => (e.IsMinion || e.IsHero) && e.IsInPlay)
                    .Sum(e => e.Attack);
            Text2.Text = $"敌方总场攻：{OppTotalAttack}";
            int PlayerTotalAttack = player.Where(e => (e.IsMinion || e.IsHero) && e.IsInPlay)
                                .Sum(e => e.Attack);
            Text3.Text = $"我方总场攻：{PlayerTotalAttack}";

            if ((OppTotalAttack - PlayerTotalAttack  > 10) && (PlayerTotalAttack <= 5) && Turn == ActivePlayer.Player)
            {
                Update("场攻差距好大！","别急");
            }
            else
            {
                DaiJi(Turn);
            }

        }
        public void Check(List<Entity> opp, List<Entity> player , ActivePlayer Turn)
        {
            UpdatePosition();
            DaiJi(Turn);


        }
        public void DaiJi(ActivePlayer NowTurn)
        {
            if (NowTurn != turn && NowTurn != ActivePlayer.None && this.Visibility == Visibility.Visible)
            {
                turn = NowTurn;
                IsDaji = true;
            }
            if (IsDaji && nowplaying == "main" && _random.NextDouble() < 0.01 && (nowplaying != "胜利" || nowplaying != "失败" || nowplaying != "平局" || nowplaying != "在自己的回合失败"))
            {
                IsDaji = false;
                nowplaying = "待机";
                PlayVideoLoop(nowplaying);
            }
        }

        private void PlayVideoLoop(string video)
        {
            string dllDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string videoPath;

            // 查找所有以“video”参数为前缀的 mp4 文件，例如 待机1.mp4、待机2.mp4 等
            string[] videos = Directory.GetFiles(dllDir, video + "*.mp4");

            if (videos.Length == 0)
            {
                // 没有匹配到多个，就尝试单个 video.mp4
                videoPath = System.IO.Path.Combine(dllDir, video + ".mp4");
                if (!System.IO.File.Exists(videoPath))
                {
                    MessageBox.Show("找不到视频文件：" + video + ".mp4");
                    return;
                }
            }
            else
            {
                // 有多个匹配，随机选一个
                videoPath = videos[_random.Next(videos.Length)];
            }
            MyMedia.Stop();
            MyMedia.Source = new Uri(videoPath, UriKind.Absolute);
            MyMedia.Play();
        }


        // 在播放结束时再次播放，实现循环
        private void MyMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (nowplaying == "胜利" || nowplaying == "失败" || nowplaying == "平局" || nowplaying == "在自己的回合输了") { 
                nowplaying = "main";
            }
            else
            {
                nowplaying = "main";
                PlayVideoLoop(nowplaying);
                
            }
            
        }


    
    }
}

