/* Author: Josiah Hoppe
 * Date: Nov 6, 2024
 * Purpose: This part of the program that creates Appointment objects from user input.
 */
namespace AppointmentNameSpace {
    public class Appointment
    {
        private string description;
        private AppointmentDate day;
        private AppointmentTime from;
        private AppointmentTime to;

        /**
           Constructs an Appointment object.
        */
        public Appointment(string s)
        {
            string[] tokens = s.Split(" ");
            int descriptionCount = tokens.Length - 3;
            description = tokens[0];
            for (int i = 1; i < descriptionCount; i++)
            {
                description += " " + tokens[i];
            }
 
            day = new AppointmentDate(tokens[tokens.Length - 3]);
            from = new AppointmentTime(tokens[tokens.Length - 2]);
            to = new AppointmentTime(tokens[tokens.Length - 1]);
        }

        /**
           Determines if this appointment is the same as another appointment.
           @param other the other appointment
           @return true if the appointments are equal, false otherwise
        */
        public override bool Equals(Object other)
        {
            /*if (other == null || !(other instanceof Appointment))
      {
                return false;
            }*/
            Appointment b = (Appointment)other;

            return description.Equals(b.description) &&
               day.Equals(b.day) &&
               from.Equals(b.from) &&
               to.Equals(b.to);
        }

        /**
           Determines if an appointment falls on a certain day.
           @param d the appointment date
           @return true if the appointment date falls on a
               certain day false, otherwise
        */
        public bool FallsOn(AppointmentDate d)
        {
            return day.Equals(d);
        }

        /**
           Return a string representation
        */
        public override string ToString()
        {
            return description + " " + day + " " + from + " " + to;
        }
    }
}