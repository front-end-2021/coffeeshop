using System;
using CoffeeShop.GlobalConstant;
using CoffeeShop.DesignPattern;

namespace CoffeeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Global.GetMenu());
            Client c = new Client();
            Staff staff = new Staff(c.OrderWhiteCoffee());
            c.Wait(staff.HasWhiteCoffee(true, Constanst.CupSize.Medium));
            c.Receive(staff.TakeCoffeeBack());

            Client c2 = new Client();
            Staff staff2 = new Staff(c2.OrderWhiteCoffee());
            c2.Wait(staff2.HasWhiteCoffee(false, Constanst.CupSize.Small));
            c2.Receive(staff2.TakeCoffeeBack());

        }
    }
}
