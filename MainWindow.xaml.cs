using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Dhruvi Lad (8809938)
    /// </summary>
    public partial class MainWindow : Window
    {
        //  List of seat class 
        List<Seat> s = new List<Seat>();

        public MainWindow()
        {
            InitializeComponent();

            // check if the output.json file is exsits or not
            if (File.Exists("output.json"))
            {

                //Read value from output.json
                string input = File.ReadAllText("output.json");
                s = JsonConvert.DeserializeObject<List<Seat>>(input);
                if (s != null)
                {
                    foreach (Seat s1 in s)
                    {
                        Button button = (Button)this.FindName("Seat" + s1.seatNo);
                        button.Background = Brushes.Red;
                        button.Content = s1.passengerName;
                    }
                }
            }
        }

        // Function for book a seat
        private void Add_button(object sender, RoutedEventArgs e)
        {
            if(seat_number_tb.Text == null)
            {
                errMsg.Content = "Please enter seat number";
                errMsg.Foreground = Brushes.Blue;

            }
            else if (customer_name.Text == null)
            {
                errMsg.Content = "Please enter your name";
                errMsg.Foreground = Brushes.Blue;
            }
            else
            {
                Seat seat = new Seat();
                seat.seatNo = seat_number_tb.Text;
                seat.passengerName = customer_name.Text;
                errMsg.Content = "";
                s.Add(seat);
                //Button button = (Button)this.FindName("Seat" + seat.seatNo);
                //if (button != null)
               // {
                   // if (bookedSeat(seat.seatNo))
                    //{
                    //    button.Background = Brushes.Red;
                    //    button.Content = seat.passengerName;
                   // }
               // }

            }           
            seat_number_tb.Text = "";
            customer_name.Text = ""; 
        }   

        // funciton for cancle reservation
        private void Remove_button(object sender, RoutedEventArgs e)
        {
            string seat_number = remove_name.Text;
            var person = s.Where(x => x.seatNo == seat_number).FirstOrDefault();
            Console.WriteLine(seat_number);
            var person2 = s.Remove(person);

            Button button = (Button)this.FindName("Seat" + person.seatNo);
            if (button != null)
            {
                button.Background = Brushes.LightGreen;
                button.Content =  person.seatNo;
            }      
            remove_name.Text = "";

        }
        // check wheather seat is booked or not 
        private bool bookedSeat(string seatNumber)
        {
            Button b = (Button)this.FindName("Seat" + seatNumber);
            if(b.Background == Brushes.Red)
            {
                seat_status.Content = "Seat is already booked";
                seat_status.Foreground = Brushes.Blue;
                return false;
            }
            return true;
        }
        // button for clear reservation
        private void clear_button(object sender, RoutedEventArgs e)
        {
            s.Clear();
            string dataJson = JsonConvert.SerializeObject(s);
            File.WriteAllText("output.json", dataJson);
            for (int i=1;i<=40;i++)
            {
                Button b = (Button)this.FindName("Seat" + i);
                if(b.Background == Brushes.Red)
                {
                    b.Background = Brushes.LightGreen;
                    b.Content = i;
                }
            }
            
        }
        // Load reserverd seat from output.json file
        private void load_seat(object sender, RoutedEventArgs e)
        {
            if (File.Exists("output.json"))
            {
                string input = File.ReadAllText("output.json");
                s = JsonConvert.DeserializeObject<List<Seat>>(input);
                if (s != null)
                {
                    foreach (Seat s1 in s)
                    {
                        Button button = (Button)this.FindName("Seat" + s1.seatNo);
                        button.Background = Brushes.Red;
                        button.Content = s1.passengerName;
                    }
                }
            }
        }
        // Save reservation in Output.json file
        private void save_reservation(object sender, RoutedEventArgs e)
        {
            string passangerjson = JsonConvert.SerializeObject(s);
            File.WriteAllText("output.json", passangerjson);

        }
    }
}
