/* Author: Josiah Hoppe
 * Date: Nov 22, 2024
 * Purpose: This defines the Appointment Calendar class and lets the user store Appointments in a calendar.
 */

using AppointmentNameSpace;

/*
   Write a program to design an appointment calendar.
   An appointment includes the date, starting time,
   ending time, and a description; for example,

      Dentist 2016/10/1 17:30 18:30
      CS1 class 2016/10/2 08:30 10:00

   Part III - complete only the AppointmentCalendar class.
   Use the AppointmentDate and AppointmentTime classes from
   Part I and the Appointment class from Part II
*/
public class AppointmentCalendar
{
    readonly List<Appointment> appointments;

    /**
       Constructs an AppointmentCalendar object.
    */
    public AppointmentCalendar()
    {
        appointments = new List<Appointment>();
    }

    /**
       Adds an appointment to the calendar.
       @param a the appointment to add
    */
    public void Add(Appointment a)
    {
        appointments.Add(a);
    }

    /**
       Cancels (removes) an appointment from the calendar.
       @param a the appointment to cancel
    */
    public void Cancel(Appointment a)
    {
        for (int i = 0; i < appointments.Count; i++)
        {
            Appointment appt = appointments[i];
            if (appt.Equals(a))
            {
                appointments.RemoveAt(i);
                return;
            }
        }
    }

    /**
       Gets all appointment for a certain date.
       @param d the date
       @return the appointments for that Day
    */
    public List<Appointment> getAppointmentsForDay(AppointmentDate d)
    {
        List<Appointment> r = new List<Appointment>();
        foreach (Appointment appt in appointments)
        {
            if (appt.FallsOn(d))
            {
                r.Add(appt);
            }
        }
        return r;
    }

    public List<Appointment> searchCalendarFor(string searchPhrase)
    {
        List<Appointment> output = new List<Appointment> ();
        string[] keywords = searchPhrase.Split(" ");
        foreach (Appointment appointment in appointments)
        {
            foreach (string word in keywords)
            {
                if ((appointment.Description).Contains(word))
                {
                    output.Add(appointment);
                }
            }
        }

        return output;
    }

}
