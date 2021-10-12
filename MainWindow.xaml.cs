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
using System.Windows.Threading;

namespace Boids
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoidManager boidManager;

        public MainWindow()
        {
            InitializeComponent();

            boidManager = new BoidManager(5, ((int)main_window.Width, (int)main_window.Height));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += RedrawBiods;
            timer.Start();


        }

        private void RedrawBiods(object sender, EventArgs e)
        {
            boid_canvas.Children.Clear();
            boidManager.RunCycle();
            foreach (Boid biod in boidManager.boids)
            {
                Ellipse current_boid = new Ellipse();
                current_boid.Stroke = new SolidColorBrush(Colors.Black);
                current_boid.StrokeThickness = 3;
                current_boid.Height = 20;
                current_boid.Width = 20;
                current_boid.Fill = new SolidColorBrush(Colors.Green);
                Canvas.SetLeft(current_boid, biod.position.X);
                Canvas.SetTop(current_boid, biod.position.Y);
                boid_canvas.Children.Add(current_boid);
            }
        }
    }
}
