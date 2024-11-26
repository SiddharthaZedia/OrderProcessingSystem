using Castle.Components.DictionaryAdapter.Xml;
using System;
using System.Collections.Generic;
using System.Globalization;

public class DateTransform
{
    public static List<string> TransformDateFormat(List<string> dates)
    {
        // throw new InvalidOperationException("Waiting to be implemented.");
        if (dates is null)
            return new List<string>();
        List<string> str = new List<string>();
        foreach (string date in dates)
        {
            try
            {
                if (!DateTime.TryParse(date, out DateTime result))
                {
                    DateTime dt = DateTime.ParseExact(date, "YYYYDDMM", CultureInfo.CurrentCulture);
                }
            }
            catch
            {
                str.Add(date);
            }
        }
        return str;
    }

    public static void Main(string[] args)
    {
        
    }
}