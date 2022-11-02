using Library_POE_PART_1;
using Prog_Poe_Part_2.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace Prog_Poe_Part_2
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        //instance
        Module mod = new Module();
        ModuleContext modContext = new ModuleContext();
        //instance
        SelfStudy self = new SelfStudy();
        SelfStudyContext selfContext = new SelfStudyContext();
        //Instance
        Course temp = new Course();
        Study studies = new Study();

        


        public Home()
        {
            InitializeComponent();
            bindGridViewOne();
            bindGridViewTwo();
            ComboBox();
            ComboBoxSelfStudy();
        }

        public void ComboBox()
        {
            cmbxCode.Items.Clear();
            var query = modContext.Modules.Where(p =>p.UserName==Library_POE_PART_1.Name.UserName).ToList();
            foreach (Module m in query)
            {
                cmbxCode.Items.Add(m.ModuleCode);
            }
            
        }

        public void ComboBoxSelfStudy()
        {
            cmbSelfStudyBox.Items.Clear();
            var query = modContext.Modules.Where(q => q.UserName == Library_POE_PART_1.Name.UserName).ToList();
            foreach (Module m in query)
            {
                cmbSelfStudyBox.Items.Add(m.ModuleCode);
            }
;        }
        

        public void bindGridViewOne()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ModuleContext"].ConnectionString;
                string commandString = String.Empty;
                using (var connect = new SqlConnection(connectionString))
                {
                    commandString = "SELECT ModuleName,ModuleCode,Credits,ClassHours,Weeks,StartDate,HrsPerWeek FROM Modules WHERE (UserName='" + Library_POE_PART_1.Name.UserName + "')"; //only show modules for a specific user
                    SqlCommand cmd = new SqlCommand(commandString, connect);
                    var dt2 = new DataTable();
                    var sda2 = new SqlDataAdapter(cmd);
                    sda2.Fill(dt2);
                    DataGrid1.ItemsSource = dt2.DefaultView;
                }
            }
            catch (Exception)
            {


            }
            

        }
    
        public void bindGridViewTwo()
        {
            try
            {
                string connectionString1 = ConfigurationManager.ConnectionStrings["SelfStudyContext"].ConnectionString;
                string commandString1 = string.Empty;
                using (SqlConnection connect1 = new SqlConnection(connectionString1))
                {

                    commandString1 = "SELECT DateOfStudy,Code,HoursStudied FROM SelfStudies WHERE (UserName='" + Library_POE_PART_1.Name.UserName + "')"; //only show modules for a specific user
                    SqlCommand cmd1 = new SqlCommand(commandString1, connect1);
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda3 = new SqlDataAdapter(cmd1);
                    sda3.Fill(dt3);
                    DataGrid2.ItemsSource = dt3.DefaultView;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
           

           
        }
        private void tb1Add_Click(object sender, RoutedEventArgs e)
        {
            // new code 
            mod.UserName = Library_POE_PART_1.Name.UserName;
            mod.ModuleName = tbName.Text;
            mod.ModuleCode = tb1Code.Text;
            mod.Credits = Convert.ToInt32(tbCredits.Text);
            mod.ClassHours = Convert.ToInt32(tbClassHrs.Text);
            mod.Weeks = Convert.ToInt32(tbWeeks.Text);
            mod.StartDate = DateTime.Parse(DatePicker2.Text);
            mod.HrsPerWeek = Calculate.CalcHoursPerWeek(mod.Credits, mod.Weeks, mod.ClassHours);

            modContext.Modules.Add(mod);
           int a = modContext.SaveChanges();
            if (a > 0)
            {
                MessageBox.Show("DATA ADDED TO THE DATABASE AND DATAGRID", "SUCCESSFULL", MessageBoxButton.OK, MessageBoxImage.Information);
                ComboBox();
                ComboBoxSelfStudy();
            }
            else
            {
                MessageBox.Show("FAILED TO INSERT DATA IN THE DATABSE AND DATAGRID", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //Displaying the users input in the first datagrid
            bindGridViewOne();
           //clears the fields for re-entry
            tbName.Clear();
            tb1Code.Clear();
            tbCredits.Clear();
            tbClassHrs.Clear();
            tbWeeks.Clear();
            DatePicker2.SelectedDate = null;

        }
        private void tb1Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter All The Information Above Then Click On Add Course!!!! ");
        }

        private void tb2Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter All The Information Above Then Click On Add Hours!!!! ");

        }

        private void tb2Add_Click(object sender, RoutedEventArgs e)
        {
            self.UserName = Library_POE_PART_1.Name.UserName;
            self.DateOfStudy = DateTime.Parse(DatePicker1.Text);
            self.Code = cmbxCode.SelectedItem.ToString();
            self.HoursStudied = Convert.ToInt32(tbHrs.Text);

            DateTime modStartDate = DateTime.Now;
            var query = modContext.Modules.Where(t => t.UserName == Library_POE_PART_1.Name.UserName && t.ModuleCode == cmbxCode.SelectedItem.ToString()).ToList();
            foreach (Module m in query)
            {
                modStartDate = m.StartDate;

            }
            self.WorkWeek = Calculate.workWeek(self.DateOfStudy, modStartDate);
            selfContext.SelfStudy.Add(self);
           int a = selfContext.SaveChanges();
            if (a > 0)
            {
                MessageBox.Show("DATA ADDED TO THE DATABASE AND DATAGRID", "SUCCESSFUL", MessageBoxButton.OK, MessageBoxImage.Information);
                bindGridViewTwo();
            }
            else
            {
                MessageBox.Show("FAILED TO INSERT DATA IN THE DATABASE AND DATAGRID", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = DateTime.Now;
            int ssHours = 0;
            int sum = 0;
            var query = modContext.Modules.Where(t => t.UserName == Library_POE_PART_1.Name.UserName && t.ModuleCode == cmbSelfStudyBox.SelectedItem.ToString()).ToList();
            foreach (Module m in query)
            {
                startDate = m.StartDate;
                ssHours = m.HrsPerWeek;
            }



            int currentweek = Calculate.currentWeek(startDate);
            var query1 = selfContext.SelfStudy.Where(a => a.UserName == Library_POE_PART_1.Name.UserName && a.Code == cmbSelfStudyBox.SelectedItem.ToString()).ToList();
            foreach  (SelfStudy s in query1)
            {
                if(currentweek == s.WorkWeek)
                {
                    sum += s.HoursStudied;
                }

            }
            int remainingHours = ssHours - sum;


            txtHours.Text = "";
            txtHours.Text  = "Self Study Remaining Hours: "+remainingHours;

        }
    }
}


