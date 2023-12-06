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
using System.Threading;
using Newtonsoft.Json;
using NewtonsoftJsonSerializer = Newtonsoft.Json.JsonSerializer;
using SystemTextJsonSerializer = System.Text.Json.JsonSerializer;


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
        public DateTime ObtenerHoraFecha()
        {
            TareaEntity Tarea = new TareaEntity();
            int hora, minuto;
            if (int.TryParse(TxtBox_Hora.Text, out hora) && int.TryParse(TxtBox_Min.Text, out minuto))
            {

                TimeSpan tiempo = new TimeSpan(hora, minuto, 0);
            }
            

            DateTime fechaSeleccionada = DatePicker.SelectedDate.HasValue ? DatePicker.SelectedDate.Value : DateTime.MinValue;
            if (int.TryParse(TxtBox_Hora.Text, out hora) && int.TryParse(TxtBox_Min.Text, out minuto))
            {
                TimeSpan tiempo = new TimeSpan(hora, minuto, 0);


                fechaSeleccionada = fechaSeleccionada.Date + tiempo;


                
            }
            return fechaSeleccionada;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var borderAnimation = FindResource("BorderAnimation") as Storyboard;
            borderAnimation?.Begin();
        }
        public List<TareaEntity> ObtenerLista()
        {
            string rutaDirectorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Entity");
            string rutaArchivo = System.IO.Path.Combine(rutaDirectorio, "Tarea.json");

            List<TareaEntity> listaDeTareas = new List<TareaEntity>();

            try
            {
                if (System.IO.File.Exists(rutaArchivo))
                {
                    string json = File.ReadAllText(rutaArchivo);
                    listaDeTareas = JsonConvert.DeserializeObject<List<TareaEntity>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }

            return listaDeTareas;
        }

        public void AgregarNuevaTarea(TareaEntity nuevaTarea)
        {
            List<TareaEntity> listaDeTareas = ObtenerLista();
            listaDeTareas.Add(nuevaTarea);

            string rutaDirectorio = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Entity");
            string rutaArchivo = System.IO.Path.Combine(rutaDirectorio, "Tarea.json");

            try
            {
                if (!System.IO.Directory.Exists(rutaDirectorio))
                {
                    System.IO.Directory.CreateDirectory(rutaDirectorio);
                }

                string jsonString = JsonConvert.SerializeObject(listaDeTareas);
                File.WriteAllText(rutaArchivo, jsonString);
                Textblock.Text = ("Archivo actualizado correctamente en: " + rutaArchivo);
            }
            catch (Exception ex)
            {
                Textblock.Text = ("Error al escribir en el archivo: " + ex.Message);
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
            string nombre = NombreTxtBox.Text; // Reemplaza esto con el valor deseado
            string descripcion = DescripTxtBox.Text; // Reemplaza esto con el valor deseado
            bool prioridad = BtnPrioridad.IsChecked.Value;
            string dot;
            
            if (prioridad)
            {
                dot = "Hola";

            }
            else {
                dot = "";
            }
            DateTime deadline = ObtenerHoraFecha(); // Reemplaza esto con el valor deseado

            TareaEntity nuevaTarea = new TareaEntity
            {
                Name = nombre,
                Description = descripcion,
                prioridad = dot,
                Deadline = deadline, 
                Due = false
            };

            AgregarNuevaTarea(nuevaTarea);
            TaskManager home = new TaskManager();
            home.Show();
            this.Close();

            // Animaciones para la nueva ventana TaskManager
            DoubleAnimation heightAnim = new DoubleAnimation(0, 450, TimeSpan.FromSeconds(0.6)); // Aumento gradual de 0 a 300
            home.BeginAnimation(Window.HeightProperty, heightAnim);
        }
    }
}
    

