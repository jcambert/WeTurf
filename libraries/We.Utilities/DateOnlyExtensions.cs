using System.Text.RegularExpressions;

namespace We.Utilities;

public static class DateOnlyExtensions
{
    
    public static DateOnly Now=>DateOnly.FromDateTime(DateTime.Now);

    public static DateOnly Min => DateOnly.MinValue;
    public static bool TryParseToDateOnly(this string value, out DateOnly result)
    {
        if(string.IsNullOrEmpty(value))
        {
            result = Min;
            return false;
        }
        if (DateOnly.TryParse(value, out result))
            return true;
        try
        {
            string year, month, day;
            int _year, _month, _day;
            string[] v = null;
            if (value.Length == 6 || value.Length == 8)
            {
                v = value.Split(2);
                (day, month, year) = (v[0], v[1], v[2]);
                if (value.Length == 6)
                {
                    var now = DateOnly.FromDateTime(DateTime.Now);
                    var decennie = ((int)now.Year / 100);
                    year = $"{decennie}{year}";
                }
                if (value.Length == 8)
                    year = $"{year}{v[3]}";
                if (!Int32.TryParse(day, out _day))
                    throw new ArgumentException($"{nameof(day)} is not a valid number");
                if (!Int32.TryParse(month, out _month))
                    throw new ArgumentException($"{nameof(month)} is not a valid number");
                if (!Int32.TryParse(year, out _year))
                    throw new ArgumentException($"{nameof(year)} is not a valid number");

                result = new DateOnly(_year, _month, _day);
                return true;
            }

            bool european = value.IndexOf('/') > 0;

            v = Regex.Split(value, "/|-");
            if (v.Length == 3)
            {

                if (!Int32.TryParse(string.Join("", european ? v[2] : v[0]), out _year))
                    throw new ArgumentException($"Malformed Date for year {value} :{string.Join("", v[0])}");
                if (!Int32.TryParse(string.Join("", v[1]), out _month))
                    throw new ArgumentException($"Malformed Date for month {value} :{string.Join("", v[1])}");
                if (!Int32.TryParse(string.Join("", european ? v[0] : v[2]), out _day))
                    throw new ArgumentException($"Malformed Date for day {value} :{string.Join("", v[2])}");
                result = new DateOnly(_year, _month, _day);
                return true;
            }

        }
        catch
        {

        }
        

        return false;
    }
}