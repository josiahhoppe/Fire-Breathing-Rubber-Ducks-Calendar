/*
    Author: Anton Tikhonov, Josiah Hoppe, Leah Proehl, Bemnet Aboye
    Current version: 3.1
    Date Modified: November 27, 2024
    Description of change 1.1 (11/14): Creating a menu that hopes us to different panels. With all of them
    
    Description of change 2.0 (11/19): Adding complete add_appointment panel. 
    Description of change 2.1 (11/19): Adding name to add_appointment panel and new panel that shows conformation after submitting 
                                       an appointment, promps to add more and if not resseting all data to default
    Description of change 2.2 (11/19): Adding drop-down box to show app, working on findint if we can put data there to show.
    Description of change 2.3 (11/25): Adding change and show panels functions. 
    Description of change 2.4 (11/25): Cleaning up the code for the implementing of classes. 
    
    Description of change 3.0 (11/25): Implementing classes. Show, Add and Edit appointment works.
    Description of change 3.1 (11/27): Search appointments works.
    Description of change 3.2 (12/03): Added Holiday and Calendar classes and implemented their logic to adding and editing appointments 


    FIX THAT
        If time is 4:00 that it will be shown as 4:0
    Potential/future to do:
        Accept not only numeric end time, but also duration.
        Other features like overlap warnings, recurring events etc
        Add checkbox for all day appointments in the Add Appointment panel
        
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
namespace calendar_2
{
    public partial class Form1 : Form
    {
        ///////////////////////////////////////////////////////////////////////////// 
        //  Initial methods:
        public Form1()
        {
            InitializeComponent();
            userCalendar.LoadHolidaysFromFile("holidays.txt");
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Creating list to store appointments
        List<Appointment> list = new List<Appointment>();
        

        ///
        Calendar userCalendar = new Calendar();
        


        ///////////////////////////////////////////////////////////////////////////// 
        //  Add appointment methods:

        int from_hour, to_hour, from_minute, to_minute;
        string description, name, date, year, month, day;
        string add_output;

        // Calling the panel from the menu
        private void add_button_Click(object sender, EventArgs e)
        { 
            add_panel.Show(); 
        }
        private void exit_add_panel_button_Click(object sender, EventArgs e)
        {
            add_panel.Hide(); 
        }

        // Getting data from user 
        private void add_app_from_minutes_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            from_minute = Convert.ToInt32(add_app_from_minutes_numericUpDown.Value);
        }
        private void add_app__from_hours_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            from_hour = Convert.ToInt32(add_app_from_hours_numericUpDown.Value);
        }
        private void add_app_to_minutes_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            to_minute = Convert.ToInt32(add_app_to_minutes_numericUpDown.Value);
        }
        private void add_app_to_hours_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            to_hour = Convert.ToInt32(add_app_to_hours_numericUpDown.Value);
        }
        private void add_app_dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date = add_app_dateTimePicker.Value.ToString("yyyy-MM-dd");
            month = add_app_dateTimePicker.Value.Month.ToString();
            day = add_app_dateTimePicker.Value.Day.ToString();
            year = add_app_dateTimePicker.Value.Year.ToString();
        }
        private void add_app_richTextBox_TextChanged(object sender, EventArgs e)
        {
            description = add_app_richTextBox.Text;
        }
        private void add_app_name_input_TextChanged(object sender, EventArgs e)
        {
            name = add_app_name_input.Text;
        }

        // Showing data at the new panel
        private void add_app_confirm_button_Click(object sender, EventArgs e)
        {
            bool fallsOnHoliday = userCalendar.fallsOnHoliday(date, out Holiday possibleHoliday);

            add_app_take_a_look_panel.Show();

            foreach (Holiday holiday in userCalendar.Holidays)
            {
                if (date.Equals(holiday.Date.ToString("yyyy-MM-dd")))
                {
                    fallsOnHoliday = true;
                    possibleHoliday = holiday;
                    break;
                }
            }
            if (!fallsOnHoliday)
            {
                add_output = String.Format("Your appointment {0} on {1} {2} starts at {3}:{4} and ends at {5}:{6}",
                name, Appointment.Month_to_string(month), day, from_hour, from_minute, to_hour, to_minute);
                add_app_take_a_look_label_1.Text = add_output;
                add_app_take_a_look_label_2.Text = "Your description: " + description;

                // Creating an object of the class and adding it to the list.
                Appointment new_app = new Appointment(from_hour, to_hour, from_minute, to_minute,
                                                       name, date, year, month, day, description);

                list.Add(new_app);
            }
            else
            {
                add_app_take_a_look_panel_welcome.Text = "An error occured while adding your appointment!";
                add_app_take_a_look_label_1.Text = $"This appointment cannot be added because it falls on the same day as {possibleHoliday.Name}";
                add_app_take_a_look_button.Text = "Try a different date";
            }
        }

        // Exiting the add appointment panels
        private void exit_add_app_take_a_look_panel_Click(object sender, EventArgs e)
        {
            add_app_take_a_look_panel_welcome.Text = "Take a look at your appointment!";
            add_app_take_a_look_button.Text = "Add another one";
            add_app_take_a_look_panel.Hide();
            add_panel.Hide();
            add_app_richTextBox.Text = "";
            add_app_name_input.Text = "";
            add_app_dateTimePicker.Value = new DateTime(2025, 1, 1);
            add_app_from_minutes_numericUpDown.Value = 0;
            add_app_to_minutes_numericUpDown.Value = 0;
            add_app_from_hours_numericUpDown.Value = 0;
            add_app_to_hours_numericUpDown.Value = 0;
        }
        private void add_app_take_a_look_button_Click(object sender, EventArgs e)
        {
            add_app_take_a_look_panel_welcome.Text = "Take a look at your appointment!";
            add_app_take_a_look_button.Text = "Add another one";
            add_app_take_a_look_panel.Hide();
        }



///////////////////////////////////////////////////////////////////////////// 
//  Edit appointment methods:
        
        int edit_index;
        string edit_date, edit_output;

        // Calling the panel from the menu
        private void edit_button_Click(object sender, EventArgs e)
        {
            edit_panel.Show();
        }
        private void edit_app_panel_button_Click(object sender, EventArgs e)
        {
            edit_panel.Hide();
        }

        // Choosing an appointment to work with
        private void edit_app_dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            edit_date = edit_app_dateTimePicker1.Value.ToString("yyyy-MM-dd");
            edit_app_comboBox1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                if (edit_date == list[i].Date)
                {
                    edit_app_comboBox1.Items.Add(list[i].Name);
                }
            }
        }
        private void edit_app_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            edit_index = edit_app_comboBox1.SelectedIndex;
            edit_app_richTextBox1.Text = list[edit_app_comboBox1.SelectedIndex].Description;
            edit_app_panel2_dateTimePicker1.Value = new DateTime(Convert.ToInt32(list[edit_app_comboBox1.SelectedIndex].Year),
                Convert.ToInt32(list[edit_app_comboBox1.SelectedIndex].Month) ,
                Convert.ToInt32(list[edit_app_comboBox1.SelectedIndex].Day));
            edit_app_panel2__to_min_numericUpDown1.Value = list[edit_app_comboBox1.SelectedIndex].To_minute;
            edit_app_panel2_to_hours_numericUpDown2.Value = list[edit_app_comboBox1.SelectedIndex].To_hour;
            edit_app_panel2_from_min_numericUpDown3.Value = list[edit_app_comboBox1.SelectedIndex].From_minute;
            edit_app_panel2_from_hours_numericUpDown4.Value = list[edit_app_comboBox1.SelectedIndex].From_hour;
        }

        // Showing data at the new panel
        private void edit_app_panel2_button3_Click(object sender, EventArgs e)
        {
            bool fallsOnHoliday = userCalendar.fallsOnHoliday(edit_app_panel2_dateTimePicker1.Value.ToString("yyyy-MM-dd"), out Holiday possibleHoliday);

            if (!fallsOnHoliday)
            {
                // Update changed data. 
                list[edit_index].To_minute = Convert.ToInt32(edit_app_panel2__to_min_numericUpDown1.Value);
                list[edit_index].To_hour = Convert.ToInt32(edit_app_panel2_to_hours_numericUpDown2.Value);
                list[edit_index].From_minute = Convert.ToInt32(edit_app_panel2_from_min_numericUpDown3.Value);
                list[edit_index].From_hour = Convert.ToInt32(edit_app_panel2_from_hours_numericUpDown4.Value);

                list[edit_index].Date = edit_app_panel2_dateTimePicker1.Value.ToString("yyyy-MM-dd");
                list[edit_index].Month = edit_app_panel2_dateTimePicker1.Value.Month.ToString();
                list[edit_index].Day = edit_app_panel2_dateTimePicker1.Value.Day.ToString();
                list[edit_index].Year = edit_app_panel2_dateTimePicker1.Value.Year.ToString();

                list[edit_index].Description = edit_app_richTextBox1.Text;


                // Showing data

                edit_output = String.Format("Your appointment {0} on {1} {2} starts at {3}:{4} and ends at {5}:{6}",
                list[edit_index].Name,
                Appointment.Month_to_string(list[edit_index].Month),
                list[edit_index].Day,
                list[edit_index].From_hour,
                list[edit_index].From_minute,
                list[edit_index].To_hour,
                list[edit_index].To_minute);

                edit_take_a_look_label2.Text = edit_output;
                edit_take_a_look_label1.Text = "Your description: " + list[edit_app_comboBox1.SelectedIndex].Description;
            }
            else
            {
                edit_take_a_look_label3.Text = "An error occured while adding your appointment!";
                edit_take_a_look_label2.Text = $"This appointment cannot be added because it falls on the same day as {possibleHoliday.Name}";
            } 
                
            
            edit_take_a_look_panel.Show();// confirm changes button

        }

        // Exiting the edit appointment panel
        private void edit_app_button1_Click(object sender, EventArgs e)
        {
            edit_app_panel2.Show();
        }
        private void edit_app_panel2_button4_Click(object sender, EventArgs e)
        {
            add_app_take_a_look_panel_welcome.Text = "Take a look at your appointment!";
            edit_panel.Hide();
            edit_app_panel2.Hide();
            edit_take_a_look_panel.Hide();
        }
        private void edit_take_a_look_button2_Click(object sender, EventArgs e)
        {
            add_app_take_a_look_panel_welcome.Text = "Take a look at your appointment!";
            edit_panel.Hide();
            edit_app_panel2.Hide();
            edit_take_a_look_panel.Hide();
        }



///////////////////////////////////////////////////////////////////////////// 
//  Search appointment methods:

        // Calling the panel from the menu
        private void search_button_Click(object sender, EventArgs e)
        {
            search_panel.Show();
        }
        private void search_app_panel_button_Click(object sender, EventArgs e)
        {
            search_panel.Hide();
        }

        // Searching an appointment
        private void search_button1_Click(object sender, EventArgs e)
        {
            search_richTextBox1.Text = "";
            string searchPhrase = search_textBox1.Text.ToLower();
            List<Appointment> output = new List<Appointment>();
            string[] keywords = searchPhrase.Split(' ');
            foreach (Appointment appointment in list)
            {
                foreach (string word in keywords)
                {
                    if ((appointment.Name.ToLower()).Contains(word) || (appointment.Description.ToLower()).Contains(word)  )
                    {
                        output.Add(appointment);
                    }
                }
            }

            foreach (Appointment appointment in output)
            {
                search_richTextBox1.Text += appointment.ToString() +"\n";
            }
        }



///////////////////////////////////////////////////////////////////////////// 
//  Show appointment methods:
        
        string show_date, show_output;

        // Calling the panel from the menu
        private void show_button_Click(object sender, EventArgs e)
        {
            show_panel.Show();
            show_comboBox1.Items.Clear();
        }
        private void show_app_panel_button_Click(object sender, EventArgs e)
        {
            show_panel.Hide();
        }

        // Choosing an appointment to work with
        private void show_app_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {   
            show_date = show_app_dateTimePicker.Value.ToString("yyyy-MM-dd");
            
            for (int i = 0; i < list.Count; i++)
            {
                if (show_date == list[i].Date)
                {
                    show_comboBox1.Items.Add(list[i].Name);
                }
            }
        }
        private void show_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            show_output = String.Format
        ("Your appointment {0} on {1} {2} starts at {3}:{4} and ends at {5}:{6}",
                    list[show_comboBox1.SelectedIndex].Name,
                    Appointment.Month_to_string(list[show_comboBox1.SelectedIndex].Month), 
                    list[show_comboBox1.SelectedIndex].Day, 
                    list[show_comboBox1.SelectedIndex].From_hour,
                    list[show_comboBox1.SelectedIndex].From_minute, 
                    list[show_comboBox1.SelectedIndex].To_hour,
                    list[show_comboBox1.SelectedIndex].To_minute);

            showing_app_label2.Text = show_output;
            showing_app_label1.Text = "Your description: " + list[show_comboBox1.SelectedIndex].Description;
        }

        // Showing data at the new panel. 
        private void show_app_button1_Click(object sender, EventArgs e)
        {
            showing_app_panel.Show();
        }

        // Exiting the show appointment panel
        private void showing_the_app_button2_Click(object sender, EventArgs e)
        {
            // Clearing data
            showing_app_label2.Text = "";
            showing_app_label1.Text = "";

            showing_app_panel.Hide();
            show_panel.Hide();
        }



        ///////////////////////////////////////////////////////////////////////////// 
        //  Cancel appointment methods:

        int cancel_index;
        string cancel_date;

        // Calling the panel from the menu
        private void cancel_button_Click(object sender, EventArgs e)
        {
            cancel_panel.Show();
        }
        private void exit_cancel_panel_button_Click(object sender, EventArgs e)
        {
            cancel_panel.Hide();
        }

        // Choosing an appointment to work with
        private void cancel_dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            cancel_date = cancel_dateTimePicker1.Value.ToString("yyyy-MM-dd");

            for (int i = 0; i < list.Count; i++)
            {
                if (cancel_date == list[i].Date)
                {
                    cancel_comboBox1.Items.Add(list[i].Name);
                }
            }
        }
        private void cancel_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cancel_index = cancel_comboBox1.SelectedIndex;
        }

        // Canceling the appointment
        private void cancel_button1_Click(object sender, EventArgs e)
        {
            list.RemoveAt(cancel_index);
            cancel_panel.Hide();
        }
    }



    ///////////////////////////////////////////////////////////////////////////// 
    // Appointment Class:
    public class Appointment
    {
        // Creating instance variables 
        private int from_hour, to_hour, from_minute, to_minute;
        private string description, name, date, year, month, day;

        // Using Auto-Implemented Properties
        public int From_hour { get; set; }
        public int From_minute { get; set; }
        public int To_hour { get; set; }
        public int To_minute { get; set; }

        public string Name { get; set; }
        public string Date { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Description { get; set; }

        // Constructor 
        public Appointment(int from_hour, int to_hour, int from_minute, int to_minute,
                            string name, string date, string year, string month,
                            string day, string description)
        {
            From_hour = from_hour;
            From_minute = from_minute;
            To_hour = to_hour;
            To_minute = to_minute;

            Name = name;
            Date = date;
            Year = year;
            Month = month;
            Day = day;

            if (description != null)
            {
                Description = description;
            } else
            {
                Description = "";
            }
           
        }

        // Method to return month as a word 
        public static string Month_to_string(string mon)
        {
            string month = "";
            switch (mon)
            {
                case "1":
                    month = "January";
                    break;
                case "2":
                    month = "February";
                    break;
                case "3":
                    month = "March";
                    break;
                case "4":
                    month = "April";
                    break;
                case "5":
                    month = "May";
                    break;
                case "6":
                    month = "June";
                    break;
                case "7":
                    month = "July";
                    break;
                case "8":
                    month = "August";
                    break;
                case "9":
                    month = "September";
                    break;
                case "10":
                    month = "October";
                    break;
                case "11":
                    month = "November";
                    break;
                case "12":
                    month = "December";
                    break;
            }
            return month;
        }

        public override string ToString()
        {
            return $"{Name} {Year}/{Month}/{Day} {From_hour}:{From_minute}-{To_hour}:{To_minute}";
        }



    }

    public class Holiday
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public Holiday(DateTime date, string name)
        {
            Date = date;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Name} on {Date.ToString("yyyy-MM-dd")}"; 
        }
    }
    
    public class Calendar
    {
        public List<Holiday> Holidays { get; set; }

        public Calendar()
        {
            Holidays = new List<Holiday>();
        }

        public void AddHoliday(Holiday holiday)
        {
            if (holiday != null)
            {
                Holidays.Add(holiday);
            }
        }

        public void LoadHolidaysFromFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            if (DateTime.TryParse(parts[0], out DateTime date))
                            {
                                string name = parts[1];
                                AddHoliday(new Holiday(date, name));
                            }
                            else
                            {
                                Console.WriteLine($"Invalid date format: {parts[0]}");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The file could not be read: {e.Message}");
            }
        }

        public bool fallsOnHoliday(String date, out Holiday possibleHoliday)
        {
            bool falls = false;
            foreach (Holiday holiday in Holidays)
            {
                if (date.Equals(holiday.Date.ToString("yyyy-MM-dd")))
                {
                    falls = true;
                    possibleHoliday = holiday;
                    return falls;
                }
            }
            possibleHoliday = null;
            return falls;
        }



    }
}