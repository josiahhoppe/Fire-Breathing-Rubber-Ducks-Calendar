/* Author: Josiah Hoppe
 * Date: Nov 6, 2024
 * Purpose: This method tests the AppointmentDate and AppointmentTime classes. The other Main methods in this project were renamed to Main1.
 */


using static System.Console;

public class AppointmentDateTimeTester
{
    public static void Main(string[] args)
    {
        WriteLine("Checking AppointmentDate class ---");
        AppointmentDate d = new AppointmentDate("2016/3/15");
        WriteLine("Checking good date: " + d);
        WriteLine("Expected: 2016/3/15");
        // bad format
        d = new AppointmentDate("Jan 15, 2016");
        WriteLine("Checking bad format: " + d);
        WriteLine("Expected: -1/-1/-1");
        WriteLine("Invalid date format: " + AppointmentDate.BAD_DATE);
        WriteLine("Expected: -1/-1/-1");
        WriteLine(d.Equals(AppointmentDate.BAD_DATE));
        WriteLine("Expected: true");
        // bad date - only 28/29 in Feb
        d = new AppointmentDate("2016/02/30");
        WriteLine("Checking invalid date: " + d);
        WriteLine("Expected: -1/-1/-1");
        d = new AppointmentDate("2016/02/02"); // groundhog day
        AppointmentDate d2 = new AppointmentDate("2016/02/02"); // also
        WriteLine("Checking equals: " + d.Equals(d2));
        WriteLine("Expected: true");
        WriteLine("Checking equals (shouldn't crash): " + d.Equals(null));
        WriteLine("Expected: false");
        WriteLine("Checking equals (shouldn't crash): " + d.Equals("2016/02/02"));
        WriteLine("Expected: false");

        WriteLine();
        WriteLine("Checking AppointmentTime class ---");
        AppointmentTime t1 = new AppointmentTime("8:15"); // 8:15 pm
        WriteLine("Checking good time: " + t1);
        WriteLine("Expected: 08:15");
        t1 = new AppointmentTime("7:05");
        WriteLine("Checking good time: " + t1);
        WriteLine("Expected: 07:05");
        WriteLine("Invalid time format: " + AppointmentTime.BAD_TIME);
        WriteLine("Expected: -1:-1");
        t1 = new AppointmentTime("7:60"); // bad time
        WriteLine("Checking bad time: " + t1);
        WriteLine("Expected: -1:-1");
        t1 = new AppointmentTime("24:05"); // bad time
        WriteLine("Checking bad time: " + t1);
        WriteLine("Expected: -1:-1");
        t1 = new AppointmentTime("8:15");
        WriteLine("Checking equals: " + t1.Equals(new AppointmentTime("8:15")));
        WriteLine("Expected: true");
        WriteLine("Checking equals (shouldn't crash): " + t1.Equals(null));
        WriteLine("Expected: false");
        WriteLine("Checking equals (shouldn't crash): " + t1.Equals("8:15"));
        WriteLine("Expected: false");
    }
}
