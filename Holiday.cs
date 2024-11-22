/* A program that allows users to add holidays to a calendar.
* 
* Author: Bemnet Aboye
* Date: 11/14/2024
*/

using System;
using System.Collections.Generic;
using System.IO;

public class Holiday
{
    public DateTime Date { get; set; }
    public string Name { get; set; }

    public Holiday(DateTime date, string name)
    {
        Date = date;
        Name = name;
    }
}

public class Calendar : IDisposable
{
    public List<Holiday> Holidays { get; set; }

    public Calendar()
    {
        Holidays = new List<Holiday>();
    }

    public void AddHoliday(Holiday holiday)
    {
        if (holiday != null)
        {
            Holidays.Add(holiday);
        }
    }

    public void LoadHolidaysFromFile(string filePath)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        if (DateTime.TryParse(parts[0], out DateTime date))
                        {
                            string name = parts[1];
                            AddHoliday(new Holiday(date, name));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid date format: {parts[0]}");
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"The file could not be read: {e.Message}");
        }
    }

    public void Dispose()
    {
        // Dispose of any resources if necessary
    }
}

public class Program
{
    public static void Main()
    {
        using (Calendar calendar = new Calendar())
        {
            // Load holidays from a file
            string filePath = "holidays.txt"; // Make sure this file exists and contains holiday data
            calendar.LoadHolidaysFromFile(filePath);

            // Display the holidays
            foreach (Holiday holiday in calendar.Holidays)
            {
                Console.WriteLine($"{holiday.Name} is on {holiday.Date.ToShortDateString()}");
            }
        }
    }
}


//  holiday.txt 
//2024 - 01 - 01,New Year's Day
//2024-07-04, Independence Day
//2024-12-25, Christmas Day
