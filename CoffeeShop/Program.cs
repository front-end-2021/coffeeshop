using CoffeeShop.DesignPattern;
using System;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Global.GetMenu());
            Client c = new Client();
            Staff s = new Staff(c.OrderWhiteCoffee());
            c.Wait(s.HasWhiteCoffee(true, Constanst.CupSize.Medium));
            c.Receive(s.TakeCoffeeBack());

        }
    }
}
