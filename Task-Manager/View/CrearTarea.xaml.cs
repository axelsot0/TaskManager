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
using System.Windows.Shapes;
using Task_Manager.Entity;
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
            Tarea.Name = NombreTxtBox.Text;
            Tarea.Description = DescripTxtBox.Text;
            if (BtnPrioridad.IsChecked == true)
            {
                Tarea.prioridad = true;
            }
            else { Tarea.prioridad = false; }
            Tarea.Deadline = DatePicker.SelectedDate.HasValue ? DatePicker.SelectedDate.Value : DateTime.MinValue;
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
            this.Close();

        }

        private void BtnRegistrarClick(object sender, RoutedEventArgs e)
        {
            NuevaTarea();
        }
    }
}
    

