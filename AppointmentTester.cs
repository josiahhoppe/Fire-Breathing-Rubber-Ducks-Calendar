//HIDE
/* Author: Josiah Hoppe
 * Date: Nov 6, 2024
 * Purpose: This part of the program tests the Appointment class. It is currently named Main1 so that the AppointmentDateTimeTester Main method runs right now.
 */

using static System.Console;
using AppointmentNameSpace;
public class AppointmentTester
{
    public static void Main1(String[] args)
    {
        WriteLine("Checking Appointment class ---");
        Appointment a3 = new Appointment("Wash the car 2016/8/1 8:5 9:15");
        WriteLine("Checking toString: " + a3);
        WriteLine("Expected: Wash the car 2016/8/1 08:05 09:15");
    }
}
