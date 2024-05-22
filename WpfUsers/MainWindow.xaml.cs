using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfUsers.Data;
using WpfUsers.Models;

namespace WpfUsers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<User> Users;
        public UsersContext context;

        public MainWindow()
        {
            InitializeComponent();
            Users= new ObservableCollection<User>();
            context= new UsersContext();

            //Init();
            RefleshUsers();

            lbUsers.ItemsSource = Users;
            spInput.DataContext = Users;
        }

        private void RefleshUsers()
        {
            Users.Clear();

            if (context.Users.Any())
                foreach (var item in context.Users)            
                    Users.Add((User)item);
            else Users.Add(new User());
            
        }


        private void Init() { 
            context.Users.Add(new User("Józsa Béla", "bela@gmail.com"));
            context.Users.Add(new User("Bálint Dezső", "bdezso@gmail.com"));
            context.SaveChanges();
        }


        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           
            User user = lbUsers.SelectedItem as User;
            if (user == null) user = new User();
            user.Id = 0;
            context.Users.Add(user);
            context.SaveChanges();
            RefleshUsers();
            lbUsers.SelectedItem= user;
            lbUsers.UpdateLayout();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = lbUsers.SelectedItem as User;
            if (user != null) {
                int index = lbUsers.SelectedIndex;

                context.Users.Remove(user); 
                context.SaveChanges();               
                RefleshUsers();

                lbUsers.SelectedIndex = index < lbUsers.Items.Count - 1 ? index : lbUsers.Items.Count - 1;
                lbUsers.UpdateLayout();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User user = lbUsers.SelectedItem as User;
            if (user != null)
            {
                context.Users.Update(user);
                context.SaveChanges();               
                RefleshUsers();
                lbUsers.SelectedItem = user;
                lbUsers.UpdateLayout();
            }
        }
    }
}
