using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
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

using System.Windows.Threading;

namespace FirstSnakeGame
{
    public partial class MainWindow : Window
    {
        bool goLeft, goRight, goUp, goDown;
        int playerSpeed = 8;
        int speed = 12;
        int score;

        DispatcherTimer gameTimer = new DispatcherTimer();
        static Random random = new Random();
        int width = random.Next(670);
        int height = random.Next(370);


        public MainWindow()
        {
            InitializeComponent();

            Area.Focus();

            int width = random.Next(670);
            int height = random.Next(370);
            Canvas.SetLeft(Apple, width);
            Canvas.SetTop(Apple, height);

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft == true && Canvas.GetLeft(SnakeHead) > -15)
            {
                Canvas.SetLeft(SnakeHead, Canvas.GetLeft(SnakeHead) - playerSpeed);
            }
            if (goRight == true && Canvas.GetLeft(SnakeHead) + (SnakeHead.Width + 20) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(SnakeHead, Canvas.GetLeft(SnakeHead) + playerSpeed);
            }
            if (goUp == true && Canvas.GetTop(SnakeHead) > 0)
            {
                Canvas.SetTop(SnakeHead, Canvas.GetTop(SnakeHead) - playerSpeed);
            }
            if (goDown == true && Canvas.GetTop(SnakeHead) + (SnakeHead.Height + 20) < Application.Current.MainWindow.Height - 15)
            {
                Canvas.SetTop(SnakeHead, Canvas.GetTop(SnakeHead) + playerSpeed);
            }

            var x1 = Canvas.GetLeft(SnakeHead);
            var y1 = Canvas.GetTop(SnakeHead);
            Rect r1 = new Rect(x1, y1, SnakeHead.ActualWidth, SnakeHead.ActualHeight);


            var x2 = Canvas.GetLeft(Apple);
            var y2 = Canvas.GetTop(Apple);
            Rect r2 = new Rect(x2, y2, Apple.ActualWidth, Apple.ActualHeight);

            if (r1.IntersectsWith(r2))
            {
                score++;
                using (var writer = new StreamWriter("../../score.txt"))
                {
                    writer.WriteLine(score);
                }
                textBlock.Text = File.ReadAllText("../../score.txt");
                int width = random.Next(670);
                int height = random.Next(370);
                Canvas.SetLeft(Apple, width);
                Canvas.SetTop(Apple, height);
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goRight = false;
                goUp = false;
                goDown = false;
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goUp = false;
                goDown = false;
                goLeft = false;
                goRight = true;
            }
            if (e.Key == Key.Up)
            {
                goDown = false;
                goLeft = false;
                goRight = false;
                goUp = true;
            }
            if (e.Key == Key.Down)
            {
                goUp = false;
                goLeft = false;
                goRight = false;
                goDown = true;
            }
        }

        //private void KeyIsUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Left)
        //    {
        //        goLeft = false;
        //    }
        //    if (e.Key == Key.Right)
        //    {
        //        goRight = false;
        //    }
        //    if (e.Key == Key.Up)
        //    {
        //        goUp = false;
        //    }
        //    if (e.Key == Key.Down)
        //    {
        //        goDown = false;
        //    }
        //}
    }
}
