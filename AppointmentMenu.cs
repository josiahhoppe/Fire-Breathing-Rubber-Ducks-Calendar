
/* Author: Josiah Hoppe
 * Date: Nov 22, 2024
 * Purpose: This section of the program serves as the UI for the program.
 */

using AppointmentNameSpace;
using static System.Console;
/**
   A menu for the appointment calendar system.
*/
public class AppointmentMenu
{
   private AppointmentCalendar calendar;

    /*
       Constructs an AppointmentMenu object.
    */
    public AppointmentMenu()
    {
        calendar = new AppointmentCalendar();
    }

    /**
       Runs the system.
    */
    public void Run()
    {
        bool more = true;

        while (more)
        {
            Write("A)dd  C)ancel  D)isplay  S)earch Q)uit : ");
            string command = ReadLine().ToUpper().Trim();

            if (command.Equals("A"))
            {
                Write("Appointment (Description Date From To) : ");
                string line = ReadLine();
                Appointment a = new Appointment(line);
                calendar.Add(a);
            }
            else if (command.Equals("C"))
            {
                Write("Appointment (Date From To) : ");
                string line = ReadLine();
                AppointmentDate Day = new AppointmentDate(line);
                Appointment a = GetChoice(calendar.getAppointmentsForDay(Day));
                if (a != null) { calendar.Cancel(a); }
            }
            else if (command.Equals("D"))
            {
                Write("Date (yyyy/mm/dd) :");
                string line = ReadLine();
                AppointmentDate Day = new AppointmentDate(line);
                foreach (Appointment appt in calendar.getAppointmentsForDay(Day))
                {
                    WriteLine(appt);
                }
            }
            else if (command.Equals("S"))
            {
                Write("Search phrase: ");
                string searchPhrase = ReadLine();
                List<Appointment> searchResults = new List<Appointment>(); 
                searchResults = calendar.searchCalendarFor(searchPhrase);

                foreach (Appointment appt in searchResults)
                {
                    WriteLine(appt);
                }
            }
            else if (command.Equals("Q"))
            {
                more = false;
            }
        }
    }

    private Appointment GetChoice(List<Appointment> choices)
    {
        if (choices.Count == 0) return null;
        while (true)
        {
            char c = 'A';
            foreach (Appointment choice in choices)
            {
                WriteLine(c + ") " + choice);
                c++;
            }
            string input = ReadLine();
            int n = input.ToUpper()[0] - 'A';
            if (0 <= n && n < choices.Count) { return choices[n]; }
        }
    }

    public static void Main(string[] args)
    {
        new AppointmentMenu().Run();
    }
}
