using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delivery_time
{
    public class Order
    {
        private readonly DateTime _time;
        public Order(DateTime time)
        {
            _time = time;
        }
        // method calculates delivery date and time
        public void printDeliveryDateTime(
            int timeFromWirehouseToTransitPoint = 2,
            int timeDeclaration = 1,
            int timeFromTransitPointToClient = 2,
            int startWorkHour = 8,
            int endWorkHour = 17,
            int endWorkDay = 5)
        {
            DateTime deliveryDateTime = new DateTime(); // result delivery date and time
            int fullDeliveryTime = timeFromWirehouseToTransitPoint + timeDeclaration + timeFromTransitPointToClient;

            // if order was placed on a weekday
            if ((int)_time.DayOfWeek <= endWorkDay)
            {
                // after 8:00
                if (_time.Hour >= startWorkHour)
                {
                    // between 8:00 and 15:00(not included)
                    if (_time.Hour < endWorkHour - timeFromWirehouseToTransitPoint)
                    {
                        // between 8:00 and 14:00(not included)
                        if (_time.Hour < endWorkHour - timeFromWirehouseToTransitPoint - timeDeclaration)
                        {
                            // between 8:00 and 12:00(not included)
                            if (_time.Hour < endWorkHour - fullDeliveryTime)
                            {
                                deliveryDateTime = _time.AddHours(fullDeliveryTime);
                                Console.WriteLine("Estimated delivery date and time: " + deliveryDateTime.ToString());
                            }
                            else calculateNotSameDayDeliveryTime(timeFromTransitPointToClient, 0, startWorkHour, endWorkDay, deliveryDateTime);
                        }
                        else calculateNotSameDayDeliveryTime(timeDeclaration + timeFromTransitPointToClient, 0, startWorkHour, endWorkDay, deliveryDateTime);
                    }
                    // after 17:00
                    if (_time.Hour >= endWorkHour - timeFromWirehouseToTransitPoint)
                        calculateNotSameDayDeliveryTime(fullDeliveryTime, 1, startWorkHour, endWorkDay, deliveryDateTime);

                }
                // before 8:00
                else if (startWorkHour > _time.Hour)
                {
                    deliveryDateTime = _time.AddHours(-_time.Hour + startWorkHour + fullDeliveryTime).AddMinutes(-_time.Minute);
                    Console.WriteLine("Estimated delivery date and time: " + deliveryDateTime.ToString());
                }
            }
            else
            // if order was placed on a holiday
            {
                int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)_time.DayOfWeek + 7) % 7;
                deliveryDateTime = _time.AddDays(daysUntilNextMonday).AddHours(-_time.Hour + startWorkHour + fullDeliveryTime).AddMinutes(-_time.Minute);
                Console.WriteLine("Estimated delivery date and time: " + deliveryDateTime.ToString());
            }
        }

        // method calculates delivery date and time, when package can't be delivered to client in the same day
        public void calculateNotSameDayDeliveryTime(int timeLeft, int nextDay, int startWorkHour, int endWorkDay, DateTime deliveryDateTime)
        {
            if ((int)_time.DayOfWeek == endWorkDay)
            {
                deliveryDateTime = _time.AddDays(3).AddHours(-_time.Hour + startWorkHour + timeLeft).AddMinutes(-_time.Minute);
                Console.WriteLine("Estimated delivery date and time: " + deliveryDateTime.ToString());
            }
            else
            {
                deliveryDateTime = _time.AddDays(nextDay).AddHours(-_time.Hour + startWorkHour + timeLeft).AddMinutes(-_time.Minute);
                Console.WriteLine("Estimated delivery date and time: " + deliveryDateTime.ToString());
            }
        }
    }
}
