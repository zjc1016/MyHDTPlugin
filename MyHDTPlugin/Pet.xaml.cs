using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Utility;
using System;
using System.Collections.Generic;
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

namespace MyHDTPlugin
{

    public partial class MyPet
    {
        public string nowplaying = "待机.mp4";
        public string nextplaying = "";

        public MyPet()
        {

            InitializeComponent();  // ✅ 只有在 x:Class 正确时才会编译这个方法
            PlayVideoLoop("待机.mp4");
        }
        public void Update(string text, string video)
        {   
            Text.Text = text;
            this.Visibility = Visibility.Visible;
            if (video != null && nowplaying == "待机.mp4")
            {
                nowplaying = video;
                PlayVideoLoop(nowplaying);
            }
            
            UpdatePosition();

        }
        public void UpdatePosition()
        {
            Canvas.SetBottom(this, Core.OverlayWindow.Height * 5 / 100);
            Canvas.SetLeft(this, Core.OverlayWindow.Width * 10 / 100);
        }
        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        public void Check()
        {

        }

        private void PlayVideoLoop(string video)
        {
            string dllDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string videoPath = System.IO.Path.Combine(dllDir, video);

            if (System.IO.File.Exists(videoPath))
            {
                MyMedia.Source = new Uri(videoPath, UriKind.Absolute);
                MyMedia.Play();
            }
            else
            {
                MessageBox.Show("找不到视频文件：" + videoPath);
            }
        }

        // 在播放结束时再次播放，实现循环
        private void MyMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (nowplaying == "胜利.mp4") { 
                nowplaying = "待机.mp4";
            }
            else
            {
                nowplaying = "待机.mp4";
                PlayVideoLoop(nowplaying);
                MyMedia.Play();
            }
            
        }

    }
}

