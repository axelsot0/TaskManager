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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task_Manager.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            
            DoubleAnimation anim = new DoubleAnimation(ActualHeight, 0, TimeSpan.FromSeconds(0.5));
            this.BeginAnimation(Window.HeightProperty, anim);
            await Task.Delay(600);
            this.Close();
            
        }

        private async void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            // Animaciones para la ventana actual
            DoubleAnimation anim = new DoubleAnimation(ActualHeight, 0, TimeSpan.FromSeconds(0.6));
            this.BeginAnimation(Window.HeightProperty, anim);

            DoubleAnimation borderOpacityAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.6));
            btnLogIn.BorderBrush.BeginAnimation(SolidColorBrush.OpacityProperty, borderOpacityAnim);

            DoubleAnimation contentOpacityAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.6));
            btnLogIn.Foreground.BeginAnimation(SolidColorBrush.OpacityProperty, contentOpacityAnim);

            await Task.Delay(600); // Esperar el tiempo de las animaciones (600ms en este caso)

            // Cerrar la ventana actual después de las animaciones


            // Mostrar la nueva ventana TaskManager
            TaskManager home = new TaskManager();
            home.Show();
            this.Close();

            // Animaciones para la nueva ventana TaskManager
            DoubleAnimation heightAnim = new DoubleAnimation(0, 450, TimeSpan.FromSeconds(0.6)); // Aumento gradual de 0 a 300
            home.BeginAnimation(Window.HeightProperty, heightAnim);
        }
    }
}
