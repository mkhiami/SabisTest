using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace SabisTest.Helpers
{
  public class Utilities
  {
    public static int GetInt(object data)
    {
      if (data == null) return 0;
      int result = 0;
      int.TryParse(data.ToString(), out result);
      return result;
    }

    public static decimal GetDecimal(object data)
    {
      if (data == null) return 0;
      decimal result = 0;
      decimal.TryParse(data.ToString(), out result);
      return result;
    }

    public static bool GetBoolean(object data, string trueCondition)
    {
      if (data == null) return false;
      bool result = data.ToString() == trueCondition;
      return result;
    }


    public static DateTime? GetDate(string data)
    {
      DateTime? result = null;
      if (!string.IsNullOrEmpty(data))
      {
        result = DateTime.Parse(data, CultureInfo.InvariantCulture);
      }
      return result;
    }

    /// <summary>
    /// Used to returf an HTML string rea from an HTML templates folder
    /// </summary>
    /// <returns>The html content.</returns>
    /// <param name="path">Path.</param>
    public static string GetHtmlContent(string path)
    {
      string data = "";
      using (StreamReader reader = new StreamReader(path))
      {
        data = reader.ReadToEnd();
      }

      return data;
    }



    /// <summary>
    /// Return weekdays withiin a given date reange
    /// </summary>
    /// <returns>The week days.</returns>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    public static int GetWeekDays(DateTime start, DateTime end)
    {
      int days = 0;
      DateTime tempDate = start;
      while (tempDate <= end)
      {
        if (tempDate.DayOfWeek != DayOfWeek.Friday && tempDate.DayOfWeek != DayOfWeek.Saturday)
        {
          days++;
        }
        tempDate = tempDate.AddDays(1);
      }
      return days;
    }



    public static bool IsArabic()
    {
      if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower().StartsWith("ar"))
        return true;
      return false;
    }


    /// <summary>
    /// Will return the added workiung day
    /// </summary>
    /// <param name="date"></param>
    /// <param name="workingDays"></param>
    /// <returns></returns>
    public static DateTime AddWorkDays(DateTime date, int workingDays)
    {
      int direction = workingDays < 0 ? -1 : 1;
      DateTime newDate = date;
      while (workingDays != 0)
      {
        newDate = newDate.AddDays(direction);
        if (newDate.DayOfWeek != DayOfWeek.Saturday &&
            newDate.DayOfWeek != DayOfWeek.Friday &&
            !IsHoliday(newDate))
        {
          workingDays -= direction;
        }
      }
      return newDate;
    }

    /// <summary>
    /// Should be configured to read from setup table
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static bool IsHoliday(DateTime date)
    {
      List<DateTime> holidays = new List<DateTime>(){ new DateTime(2018,01,01) };

      return holidays.Contains(date.Date);
    }
  }//end Utilities Class
}//end Namespace
