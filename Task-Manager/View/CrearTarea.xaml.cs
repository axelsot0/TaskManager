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
using Task_Manager.Entity;
using System.Text.Json;
using System.IO;
namespace Task_Manager.View
{
    /// <summary>
    /// Lógica de interacción para CrearTarea.xaml
    /// </summary>
    public partial class CrearTarea : Window
    {
        public CrearTarea()
        {
            InitializeComponent();
            DatePicker.DisplayDateStart = DateTime.Today;
        }
        public void borrarCampos()
        {
            NombreTxtBox.Text = string.Empty;
            DescripTxtBox.Text = string.Empty;
            BtnPrioridad.IsChecked = false;
            TxtBox_Hora.Text = string.Empty;
            TxtBox_Min.Text = string.Empty;
            DatePicker.Text = string.Empty;

        }

        public void NuevaTarea()
        {

            TareaEntity Tarea = new TareaEntity();
            {
                Tarea.Name = NombreTxtBox.Text;
                Tarea.Description = DescripTxtBox.Text;
                if (BtnPrioridad.IsChecked == true)
                {
                    Tarea.prioridad = true;
                }
                else { Tarea.prioridad = false; }

                int hora, minuto;
                if (int.TryParse(TxtBox_Hora.Text, out hora) && int.TryParse(TxtBox_Min.Text, out minuto))
                {
                    
                    TimeSpan tiempo = new TimeSpan(hora, minuto, 0); 
                }
                else
                {

                }

                    DateTime fechaSeleccionada = DatePicker.SelectedDate.HasValue ? DatePicker.SelectedDate.Value : DateTime.MinValue;
                if (int.TryParse(TxtBox_Hora.Text, out hora) && int.TryParse(TxtBox_Min.Text, out minuto))
                {
                    TimeSpan tiempo = new TimeSpan(hora, minuto, 0); 

                    
                    fechaSeleccionada = fechaSeleccionada.Date + tiempo;

                    
                    Tarea.Deadline = fechaSeleccionada;
                }



            }
            string nombreDirectorio = "Entity";
            string nombreArchivo = "persona.json";

            // Ruta del directorio de datos de la aplicación
            string rutaDirectorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nombreDirectorio);

            // Ruta completa del archivo
            string rutaArchivo = System.IO.Path.Combine(rutaDirectorio, nombreArchivo);

            try
            {
                // Si el directorio no existe, créalo
                if (!Directory.Exists(rutaDirectorio))
                {
                    Directory.CreateDirectory(rutaDirectorio);
                }

                // Ahora intenta escribir el archivo
                File.WriteAllText(rutaArchivo, "Datos que quieres escribir en el archivo...");
                Console.WriteLine("Archivo creado correctamente en: " + rutaArchivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir en el archivo: " + ex.Message);
            }






        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

            borrarCampos();
            TaskManager home = new TaskManager();
            home.Show();
            this.Close();

            // Animaciones para la nueva ventana TaskManager
            DoubleAnimation heightAnim = new DoubleAnimation(0, 450, TimeSpan.FromSeconds(0.6)); // Aumento gradual de 0 a 300
            home.BeginAnimation(Window.HeightProperty, heightAnim);

        }

        private void BtnRegistrarClick(object sender, RoutedEventArgs e)
        {
            NuevaTarea();
            TaskManager home = new TaskManager();
            home.Show();
            this.Close();

            // Animaciones para la nueva ventana TaskManager
            DoubleAnimation heightAnim = new DoubleAnimation(0, 450, TimeSpan.FromSeconds(0.6)); // Aumento gradual de 0 a 300
            home.BeginAnimation(Window.HeightProperty, heightAnim);
        }
    }
}
    

