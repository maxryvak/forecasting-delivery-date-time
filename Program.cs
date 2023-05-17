using System;

namespace delivery_time;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nEnter the date and time (format: dd.mm.yyyy hh:mm):");
            string orderTimeStr = Console.ReadLine()!;
            DateTime orderDateTime = new DateTime();
            try
            {
                orderDateTime = DateTime.ParseExact(orderTimeStr, "dd.MM.yyyy HH:mm", null);
            }
            catch (FormatException) 
            { 
                Console.WriteLine("Invalid date and time format.");
                continue;
            }
            Order order = new Order(orderDateTime);
            order.printDeliveryDateTime();
        }
    }
}