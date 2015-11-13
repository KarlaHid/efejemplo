using System;
using ejercicio01.MiBD;
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
using System.Text.RegularExpressions;

namespace ejercicio01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          if(Regex.IsMatch(nombre.Text, "^[a-zA-Z]+$") && Regex.IsMatch(sueldo.Text, @"^\d+$")){
            //instanciar bd
            demoEF db = new demoEF();
            Empleado emp = new Empleado();
            emp.Nombre = nombre.Text;
            emp.Sueldo = int.Parse(sueldo.Text);
           // emp.Departamento_id = (int)com1.SelectedValue;

            db.Empleados.Add(emp);
            db.SaveChanges();
          }
          else { MessageBox.Show("Ingresa solo letras en @Nombre y numeros en @Sueldo "); }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(idtext.Text, @"^\d+$") && Regex.IsMatch(nombre.Text, "^[a-zA-Z]+$") && Regex.IsMatch(sueldo.Text, @"^\d+$"))
            {

                demoEF db = new demoEF();
                int id = int.Parse(idtext.Text);
                var emp = db.Empleados.SingleOrDefault(x => x.id == id);
                //where x.id == id
                //select x;
                if (emp != null)
                {
                    emp.Nombre = nombre.Text;
                    emp.Sueldo = int.Parse(sueldo.Text);
                    db.SaveChanges();
                }

            }//if
            else { MessageBox.Show("Datos invalidos, ingresa los caracteres correctos"); }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(Regex.IsMatch(idtext.Text, @"^\d+$")) {

             demoEF db = new demoEF();
            int id = int.Parse(idtext.Text);
            var emp = db.Empleados.SingleOrDefault(x => x.id == id);
                      //where x.id == id
                      //select x;
            if (emp != null)
            {
                db.Empleados.Remove(emp);
                    db.SaveChanges();
            }
            else { MessageBox.Show("Solo numeros #id"); }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            demoEF db = new demoEF();
            int id = int.Parse(idtext.Text);
            var registros = from s in db.Empleados
                            where s.id== id
                            select new{
                                s.Nombre,
                                s.Sueldo
                            };
            dbgrid.ItemsSource = registros.ToList();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            demoEF db = new demoEF();

            var registros = from s in db.Empleados

                            select s;
            dbgrid.ItemsSource = registros.ToList();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(depas.Text, "^[a-zA-Z]+$") )
            {
                //instanciar bd
                demoEF db = new demoEF();
                Departamento deps = new Departamento();
                deps.Nombre = depas.Text;
                

                db.Departamentos.Add(deps);
                db.SaveChanges();
            }
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            demoEF db = new demoEF();
            com1.ItemsSource = db.Departamentos.ToList();
            com1.DisplayMemberPath = "Nombre";
            com1.SelectedValuePath = "id";

        }
    }
}
