
/* Author: Josiah Hoppe
 * Date: Nov 6, 2024
 * Purpose: This section of the code stores appointment times, and makes sure the times are valid.
 */

/**
   Write a program to design an appointment calendar.
   An appointment includes the date, starting time,
   ending time, and a description; for example,

      Dentist 2016/10/1 17:30 18:30
      CS1 class 2016/10/2 08:30 10:00

   Part I - complete only the AppointmentDate and
   AppointmentTime classes. Use the format -1/-1/-1 for
   a bad date and -1:-1 for a bad time.
*/
public class AppointmentTime
{
    private int hours;
    private int minutes;
    private bool allDay = false;

    public static readonly AppointmentTime BAD_TIME = new AppointmentTime("-1:-1");

    /**
       Constructs an AppointmentTime object
       @param t the time
    */
    public AppointmentTime(string t)
    {
       //allDay will be altered by a checkbox on the add_appointment panel, so this cannot be complete until we combine the GUI with the classes
       if(allDay)
       {
          hours = 24;
          minutes = 60;
       }
       else
       {
          string[] time = t.Split(":");
          try
          {
             hours = Convert.ToInt32(time[0]);
             minutes = Convert.ToInt32(time[1]);
             if (hours < 0 || hours > 23) { throw (new Exception()); }
             if (minutes < 0 || minutes > 59) { throw (new Exception()); }
          }
          catch (Exception e)
          {
             hours = minutes = -1;
          }
    }

    /**
       Determines if the appointment times are equal.
       @param other the other time
       @return true if the appointment times are equal,
            false otherwise
    */
    public override bool Equals(Object other)
    {
        if (other == null)
            return false;
        if (this.GetType() != other.GetType())
            return false;
        AppointmentTime b = (AppointmentTime)other;
        return hours == b.hours && minutes == b.minutes;
    }

    /**
       Prints a string representation of the time.
       @return the time
    */
    public override string ToString()
    {
        string hoursOut = "0" + hours.ToString();
        
        hoursOut = hoursOut.Substring(hoursOut.Length - 2,2);

        string minutesOut = "0" + minutes.ToString();

        minutesOut = minutesOut.Substring(minutesOut.Length -2,2);

        return $"{hoursOut}:{minutesOut}";

    }
}
