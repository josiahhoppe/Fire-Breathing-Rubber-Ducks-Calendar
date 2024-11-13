
/* Author: Josiah Hoppe
 * Date: Nov 6, 2024
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

    /**
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
            Write("A)dd  C)ancel  S)how  Q)uit : ");
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
                AppointmentDate day = new AppointmentDate(line);
                Appointment a = GetChoice(calendar.getAppointmentsForDay(day));
                if (a != null) { calendar.Cancel(a); }
            }
            else if (command.Equals("S"))
            {
                Write("Date (yyyy/mm/dd) :");
                string line = ReadLine();
                AppointmentDate day = new AppointmentDate(line);
                foreach (Appointment appt in calendar.getAppointmentsForDay(day))
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
        if (choices.Count() == 0) return null;
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
            if (0 <= n && n < choices.Count()) { return choices[n]; }
        }
    }

    public static void Main1(string[] args)
    {
        new AppointmentMenu().Run();
    }
}
