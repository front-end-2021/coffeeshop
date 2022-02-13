using System;
using CoffeeShop.GlobalConstant;
using CoffeeShop.DesignPattern;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CoffeeShop
{
	class Program
	{
		static async Task Main(string[] args)
		{
			List<Task> tasks = new List<Task>();
			Console.WriteLine(Global.GetMenu());
			Client c = new(Constanst.Menu.WhiteCoffeeHot, Constanst.CupSize.Small);
			Staff staff = new(c.GetOrder());
			if(staff.HasResource())
            {
				tasks.Add(staff.TakingTo(c));
			}

			Client c2 = new(Constanst.Menu.WhiteCoffeeIce, Constanst.CupSize.Medium);
			staff = new(c2.GetOrder());
			if(staff.HasResource())
            {
				tasks.Add(staff.TakingTo(c2));
			}
			
			while (tasks.Count > 0)
            {
				Task finishedTask = await Task.WhenAny(tasks);

				tasks.Remove(finishedTask);
			}
		}
	}
	public class Client : IClient
	{
        readonly Order order = new();
		public Order GetOrder()
        {
			return order;

		}
		public Client(Constanst.Menu menu, Constanst.CupSize cupSize)
        {
			order.Menu = menu;
			order.CupSize = cupSize;
        }
		public void Receive(ICoffeeDisplay coffee)
		{
			if (coffee == null) return;
			Console.WriteLine("Let check order: " + coffee.Display());
		}
	}
	public class Staff
	{
        readonly Order order;
		readonly ICoffeeFactory factory; 
		ICoffee coffee;
		public Staff(Order order)
        {
			this.order = order;
			switch (order.Menu)
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
		public bool HasResource()
		{
			if (factory == null) return false;
			switch (order.Menu)
			{
				case Constanst.Menu.WhiteCoffeeHot:
					coffee = factory.CreateWhiteCoffeeHot(order.CupSize);
					break;
				case Constanst.Menu.WhiteCoffeeIce:
					coffee = factory.CreateWhiteCoffeeIce(order.CupSize);
					break;
				case Constanst.Menu.BlackCoffeeHot:
				case Constanst.Menu.BlackCoffeeIce:
				case Constanst.Menu.MilkCoffeeHot:
				case Constanst.Menu.MilkCoffeeIce:
					
					break;
			}
			bool hasResource = coffee != null ? coffee.HasResource() : false;
			if(!hasResource)
				Console.WriteLine($"Sorry, has not {ExtensionMethod.GetStringValue(order.Menu)} with {order.CupSize} at the moment");
			return hasResource;
		}
		public async Task TakingTo(Client c)
        {
			await coffee.Make(); // making
			c.Receive(coffee.Ready());
		}
	}
}
