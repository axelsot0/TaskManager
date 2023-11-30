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
    /// Lógica de interacción para TaskManager.xaml
    /// </summary>
    public partial class TaskManager : Window
    {
        public TaskManager()
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

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginView home = new LoginView();
                home.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la nueva ventana: " + ex.Message);
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            CrearTarea VentanaCrear = new CrearTarea();

            VentanaCrear.Show();

            DoubleAnimation heightAnim = new DoubleAnimation(0, 450, TimeSpan.FromSeconds(0.6)); // Aumento gradual de 0 a 300
            VentanaCrear.BeginAnimation(Window.HeightProperty, heightAnim);
        }
    }
}
