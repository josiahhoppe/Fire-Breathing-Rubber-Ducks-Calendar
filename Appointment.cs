/* Author: Josiah Hoppe
 * Date: Nov 15, 2024
 * Purpose: This part of the program that creates Appointment objects from user input.
 */

namespace AppointmentNameSpace {
    public class Appointment
    {
        private AppointmentDate Day { get; set; }
        private AppointmentTime From { get; set; }
        private AppointmentTime To { get; set; }

        public string Description {get ; set ;}
        /**
           Constructs an Appointment object.
        */
        public Appointment(string s)
        {
            string[] tokens = s.Split(' ');
            int descriptionCount = tokens.Length - 3;
            Description = tokens[0];
            for (int i = 1; i < descriptionCount; i++)
            {
                Description += " " + tokens[i];
            }
 
            Day = new AppointmentDate(tokens[tokens.Length - 3]);
            From = new AppointmentTime(tokens[tokens.Length - 2]);
            To = new AppointmentTime(tokens[tokens.Length - 1]);
        }

        /**
           Determines if this appointment is the same as another appointment.
           @param other the other appointment
           @return true if the appointments are equal, false otherwise
        */
        public override bool Equals(Object other)
        {
            if (other == null)
                return false;
            Appointment b = (Appointment)other;

            return Description.Equals(b.Description) &&
               Day.Equals(b.Day) &&
               From.Equals(b.From) &&
               To.Equals(b.To);
        }

        /**
           Determines if an appointment falls on a certain Day.
           @param d the appointment date
           @return true if the appointment date falls on a
               certain Day false, otherwise
        */
        public bool FallsOn(AppointmentDate d)
        {
            return Day.Equals(d);
        }

        /**
           Return a string representation
        */
        public override string ToString()
        {
            return Description + " " + Day + " " + From + " " + To;
        }

        public override int GetHashCode()
        {
            return (Description + " " + Day + " " + From + " " + To).GetHashCode();
        }
    }
}