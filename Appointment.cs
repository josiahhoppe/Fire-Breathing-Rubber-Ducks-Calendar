/* Author: Josiah Hoppe
 * Date: Nov 6, 2024
 * Purpose: This part of the program that creates Appointment objects from user input.
 */
using System;
using System.Collections.Generic;
using System.Runtime;
namespace AppointmentNameSpace {
    public enum RecurrenceRule
    {
        None,       
        Daily,      
        Weekly,     
        Monthly,    
       
    }
    public class Appointment
    {
        public string description { get; private set; } 
        private AppointmentDate day { get; set; }
        private AppointmentTime from { get; set; }
        private AppointmentTime to { get; set; }

        public  RecurrenceRule Recurrence { get; set; }


        /**
           Constructs an Appointment object.
        */
        public Appointment(string s)
        {
            string[] tokens = s.Split(' ');
            if (tokens.Length < 4)
            {
                throw new ArgumentException("Invalid input format. Expected: Description yyyy/MM/dd HH:mm HH:mm [RecurrenceRule]");
            }

            int descriptionCount = tokens.Length - 4;

            if (Enum.TryParse(tokens[tokens.Length - 1], true, out RecurrenceRule tempRecurrence))
            {
                Recurrence = tempRecurrence;
                descriptionCount--;
            }
            else
            {
                Recurrence = RecurrenceRule.None;
            }

            description = string.Join(" ", tokens, 0, descriptionCount);
            day = new AppointmentDate(tokens[descriptionCount]);
            from = new AppointmentTime(tokens[descriptionCount + 1]);
            to = new AppointmentTime(tokens[descriptionCount + 2]);
        }

        /**
           Determines if this appointment is the same as another appointment.
           @param other the other appointment
           @return true if the appointments are equal, false otherwise
        */
        public override bool Equals(object other)
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
        public override int GetHashCode()
        {
            return HashCode.Combine(description, day, from, to);
        }
        /**
           Determines if an appointment falls on a certain day.
           @param d the appointment date
           @return true if the appointment date falls on a
               certain day false, otherwise
        */
        public bool FallsOn(AppointmentDate d)
        {
            if (day.Equals(d)) return true;

            DateTime start = day.ToDateTime();
            DateTime target = d.ToDateTime();

            switch (Recurrence)
            {
                case RecurrenceRule.Daily:
                    return start <= target;
                case RecurrenceRule.Weekly:
                    return start <= target && (target - start).Days % 7 == 0;
                case RecurrenceRule.Monthly:
                    return start <= target && start.Day == target.Day;
         
                default:
                    return false;
            }
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