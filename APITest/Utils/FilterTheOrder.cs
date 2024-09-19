using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.Utils
{
    public class FilterTheOrder
    {
        public static int CountTheOrders(string orderDate)
        {

            var formatedDate = orderDate.Replace("GMT", "").Trim();
            string format = "ddd, d MMM yyyy HH:mm:ss";

            DateTime parsedDate = DateTime.ParseExact(formatedDate, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

            DateTime currentDate = DateTime.UtcNow;

/*            Console.WriteLine("Parsed DateTime: " + parsedDate);
            Console.WriteLine("Current DateTime: " + currentDate);*/

            int comparisonResult = DateTime.Compare(parsedDate, currentDate);

            if (comparisonResult < 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
