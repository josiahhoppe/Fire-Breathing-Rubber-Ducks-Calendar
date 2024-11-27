
using System;

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
public class AppointmentDate
{
    private int year;
    private int month;
    private int day;
    private static readonly int[] DAYS = { 31, 29, 31, 30, 31, 30, 31, 30, 31, 31, 30, 31 };
    /**
     * Representation of a bad date.
     */
    public static readonly AppointmentDate BAD_DATE = new AppointmentDate("-1/-1/-1");

    /**
       Constructs an AppointmentDate object.
       @param d the date
    */
    public AppointmentDate(string d)
    {

        string[] dateArray = d.Split('/');
 
        try
        {
            year = Convert.ToInt32(dateArray[0]);
            month = Convert.ToInt32(dateArray[1]);
            day = Convert.ToInt32(dateArray[2]);

            if (month < 1 || month > 12) { throw(new Exception()); }
            if (day < 1) { throw (new Exception()); }
            if (day > DAYS[month - 1]) { throw (new Exception()); }
            if (month == 2 && !IsLeapYear(year) && day > 28) { throw (new Exception()); }
        }
        catch (Exception)
        {
            year = -1;
            month = -1;
            day = -1;
        }

    }

    public AppointmentDate(DateTime date)
    {
        year = date.Year;
        month = date.Month;
        day = date.Day;
    }

    /**
       Determines if dates are equal.
       @param the other date
       @return true if the dates are equal, false otherwise
    */
    public override bool Equals(object other)
    {
        if (other == null)
            return false;
        if (this.GetType() != other.GetType())
            return false;
        AppointmentDate b = (AppointmentDate)other;
        return year == b.year && month == b.month && day == b.day;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(year, month, day);
    }
    /**
       Prints a string representation of the date.
       @return the date
    */
    public override string ToString()
    {
        return year + "/" + month + "/" + day;
    }
    public DateTime ToDateTime()
    {
        if (!IsValid())
            throw new InvalidOperationException("Cannot convert an invalid date to DateTime.");
        return new DateTime(year, month, day);
    }
    public bool IsValid()
    {
        return year > 0 && month > 0 && day > 0;
    }
    // From "The Art and Science of Java" by Roberts.
    private static bool IsLeapYear(int year)
    {
        return ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0));
    }
}
