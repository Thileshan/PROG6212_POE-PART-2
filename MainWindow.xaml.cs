using Prog_Poe_Part_2.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library_POE_PART_1;
using System.Security.Cryptography;

namespace Prog_Poe_Part_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginContext logContext = new LoginContext();
        ModuleContext moduleContext = new ModuleContext();
        SelfStudyContext selfContext = new SelfStudyContext();
        Login user = new Login();
       
        public MainWindow()
        {
            InitializeComponent();
            logContext.Logins.ToList();
            moduleContext.Modules.ToList();
            selfContext.SelfStudy.ToList();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            user.UserName = tbUser.Text;
            user.Password = GetHashPassWord.GetHashString(tbPassword.Text);

            logContext.Logins.Add(user);
            logContext.SaveChanges();

            MessageBox.Show("YOU CAN NOW LOG INTO THE TIME MANAGEMENT APP","REGISTERED SUCCESSFULLY", MessageBoxButton.OK, MessageBoxImage.Information);

            tbUser.Clear();
            tbPassword.Clear();
            tbUser.Text = "USERNAME";
            tbPassword.Text = "PASSWORD";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var con = logContext.Logins.Find(tbUser1.Text);
            
            if (con == null)
             {
                MessageBox.Show("Account Not Found", "Failed!!!!", MessageBoxButton.OK, MessageBoxImage.Error);
             }
            

          else if (con.Password != GetHashPassWord.GetHashString(tbPassWord1.Text))
                {
                    MessageBox.Show("The Username and Password do not match!!!!","Incorrect", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (con.Password == GetHashPassWord.GetHashString(tbPassWord1.Text))
                {
                    MessageBox.Show("Logged In","Successful!!!!",MessageBoxButton.OK,MessageBoxImage.Information);

                  Library_POE_PART_1.Name.UserName = tbUser1.Text;
                Properties.Settings.Default.StudentNumber = tbUser1.Text;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();
                    Home h = new Home();
                    this.Hide();
                    h.Show();
                  

                }
               
            }

        private void tbUser1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbUser1.Text == "UserName")
            {
                tbUser1.Text = "";
                tbUser1.FontStyle = FontStyles.Oblique;
            }
        }

        private void tbUser1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbUser1.Text == "")
            {
                tbUser1.Text = "UserName";
            }
        }

        private void tbPassWord1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassWord1.Text == "PassWord")
            {
                tbPassWord1.Text = "";
                tbPassWord1.FontStyle = FontStyles.Oblique;

            }
        }

        private void tbPassWord1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassWord1.Text == "")
            {
                tbPassWord1.Text = "PassWord";
                tbPassWord1.FontStyle = FontStyles.Oblique;
            }
        }

        private void tbUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbUser.Text == "UserName")
            {
                tbUser.Text = "";
                tbUser.FontStyle = FontStyles.Oblique;

            }
        }

        private void tbUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbUser.Text == "")
            {
                tbUser.Text = "UserName";
                tbUser.FontStyle= FontStyles.Oblique;   

            }
        }

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == "PassWord")
            {
                tbPassword.Text = "";
                tbPassword.FontStyle = FontStyles.Oblique;

            }
        }

        private void tbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == "")
            {
                tbPassword.Text = "PassWord";
                tbPassword.FontStyle= FontStyles.Oblique;
            }
        }
    }
    }

