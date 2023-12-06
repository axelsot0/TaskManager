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
        private void LoadDataGrid()
        {
            try
            {
                string directorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Entity");
                string rutaArchivo = System.IO.Path.Combine(directorio, "Tarea.json");

                if (!System.IO.Directory.Exists(directorio))
                {
                    System.IO.Directory.CreateDirectory(directorio);
                }
                
                else
                {
                    string json = File.ReadAllText(rutaArchivo);

                    
                    List<TareaEntity> tareas = JsonConvert.DeserializeObject<List<TareaEntity>>(json);

                    
                    Tabla.ItemsSource = tareas; 
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al cargar datos: " + ex.Message);
            }
        }
        public void BorrarTarea()
        {
            if(Tabla.SelectedItem != null)
    {
                TareaEntity tareaSeleccionada = Tabla.SelectedItem as TareaEntity;

                try
                {
                    // Eliminar la tarea seleccionada del DataGrid
                    (Tabla.ItemsSource as List<TareaEntity>).Remove(tareaSeleccionada);
                    Tabla.ItemsSource = null;
                    // Obtener la lista de tareas del archivo JSON
                    string directorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Entity");
                    string rutaArchivo = System.IO.Path.Combine(directorio, "Tarea.json");

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        string json = File.ReadAllText(rutaArchivo);
                        List<TareaEntity> tareas = JsonConvert.DeserializeObject<List<TareaEntity>>(json);

                        // Remover la tarea seleccionada de la lista de tareas
                        tareas.Remove(tareaSeleccionada);

                        // Actualizar el archivo JSON con la lista de tareas actualizada
                        string nuevaJson = JsonConvert.SerializeObject(tareas);
                        File.WriteAllText(rutaArchivo, nuevaJson);

                        // Volver a cargar los datos en el DataGrid
                        Tabla.ItemsSource = tareas;
                        Tabla.Items.Refresh(); // Actualizar el DataGrid
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al borrar la tarea: " + ex.Message);
                }
            }
        }



        public TaskManager()
        {
            InitializeComponent();
            LoadDataGrid();
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

        private void ButtonBorrar_Click(object sender, RoutedEventArgs e)
        {
            BorrarTarea();
            LoadDataGrid();
        }
    }
}
