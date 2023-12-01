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
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using Task_Manager.Entity;
using System.Threading;

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
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            
                // Utilizando System.IO.Path para manejar rutas de archivo
            string rutaDirectorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Entity");
            string rutaArchivo = System.IO.Path.Combine(rutaDirectorio, "Tarea.json");
            if (!System.IO.Directory.Exists(rutaDirectorio))
            {
                // Lee el contenido del archivo JSON
                string jsonFilePath = rutaArchivo; // Reemplaza con la ruta real
                string json = File.ReadAllText(jsonFilePath);

                // Convierte el JSON a una lista de objetos (suponiendo que sean objetos con Id, Name y Age)
                List<TareaEntity> Tareas = JsonConvert.DeserializeObject<List<TareaEntity>>(json);

                // Asigna la lista como origen de datos para el DataGrid
                Tabla.ItemsSource = Tareas;
            }
            
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

            await Task.Delay(600);
            this.Close();

        }
    }
}
