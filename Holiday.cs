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

public class Calendar
{
    public List<Holiday> Holidays { get; set; }

    public Calendar()
    {
        Holidays = new List<Holiday>();
    }

    public void AddHoliday(Holiday holiday)
    {
        Holidays.Add(holiday);
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
                        DateTime date = DateTime.Parse(parts[0]);
                        string name = parts[1];
                        AddHoliday(new Holiday(date, name));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"The file could not be read: {e.Message}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Calendar calendar = new Calendar();

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


//  holiday.txt 
//2024 - 01 - 01,New Year's Day
//2024-07-04, Independence Day
//2024-12-25, Christmas Day
