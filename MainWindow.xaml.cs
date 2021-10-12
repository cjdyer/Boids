using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            boidManager = new BoidManager(200, ((int)main_window.Width, (int)main_window.Height));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += RedrawBiods;
            timer.Start();
        }

        private void RedrawBiods(object sender, EventArgs e)
        {
            boid_canvas.Children.Clear();
            boidManager.RunCycle();
            foreach (Boid boid in boidManager.boids)
            {
                Ellipse current_boid = new Ellipse();
                current_boid.Stroke = new SolidColorBrush(Colors.Black);
                current_boid.StrokeThickness = 1;
                current_boid.Height = 5;
                current_boid.Width = 5;
                current_boid.Fill = new SolidColorBrush(Colors.Green);
                Canvas.SetLeft(current_boid, boid.position.X);
                Canvas.SetTop(current_boid, boid.position.Y);
                boid_canvas.Children.Add(current_boid);
            }
        }
    }
}
