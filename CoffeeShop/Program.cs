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
            Client c = new();
            Staff staff = new(c.OrderWhiteCoffee(true));
            c.Wait(staff.HasWhiteCoffee(Constanst.CupSize.Small));
            c.Receive(staff.TakeCoffeeBack());

            Client c2 = new();
            Staff staff2 = new(c2.OrderWhiteCoffee(false));
            c2.Wait(staff2.HasWhiteCoffee(Constanst.CupSize.Medium));
            c2.Receive(staff2.TakeCoffeeBack());

        }
    }
	public class Client
	{
		bool hasWait = false;
		public Constanst.Menu OrderWhiteCoffee(bool isHot)
		{
			if (isHot)
				return Constanst.Menu.WhiteCoffeeHot;
			return Constanst.Menu.WhiteCoffeeIce;
		}
		public void Wait(bool hasResouce)
		{
			if (!hasResouce) Console.WriteLine("Sorry, has not at the moment");
			else hasWait = hasResouce;
		}
		public void Receive(ICoffeeDisplay coffee)
		{
			if (!hasWait || coffee == null) return;
			Console.WriteLine("Let check order: " + coffee.Display());
		}
	}
	public class Staff
	{
		readonly ICoffeeFactory factory; ICoffee coffee;
		readonly Constanst.Menu menu;

		public Staff(Constanst.Menu menu)
		{
			this.menu = menu;
			switch (menu)
			{
				case Constanst.Menu.WhiteCoffeeHot:
				case Constanst.Menu.WhiteCoffeeIce:
				case Constanst.Menu.BlackCoffeeHot:
				case Constanst.Menu.BlackCoffeeIce:
				case Constanst.Menu.MilkCoffeeHot:
				case Constanst.Menu.MilkCoffeeIce:
					factory = new CoffeeFactory();
					break;
			}
		}
		public bool HasWhiteCoffee(Constanst.CupSize size)
		{
			if (factory == null) return false;
			if (menu == Constanst.Menu.WhiteCoffeeIce)
				coffee = factory.CreateWhiteCoffeeIce(size);
			if (menu == Constanst.Menu.WhiteCoffeeHot)
				coffee = factory.CreateWhiteCoffeeHot(size);
			return coffee.HasResource();
		}
		public ICoffeeDisplay TakeCoffeeBack()
		{
			coffee.Make();      // waiting
			return coffee.Ready();
		}
	}
}
