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
			List<Task<IClient>> tasks = new();
			Console.WriteLine(Global.GetMenu());
			Client c = new(Constanst.Menu.WhiteCoffeeHot, Constanst.CupSize.Small);
			Staff staff = new(c.GetOrder());
			if(staff.HasResource())
            {
				tasks.Add(staff.TakingTo(c));
			}

			Client c2 = new(Constanst.Menu.WhiteCoffeeIce, Constanst.CupSize.Medium);
			staff.GetOrder(c2.GetOrder());
			if(staff.HasResource())
            {
				tasks.Add(staff.TakingTo(c2));
			}
			Client c3 = new(Constanst.Menu.BlackCoffeeIce, Constanst.CupSize.Small);
			staff.GetOrder(c3.GetOrder());
			if(staff.HasResource())
            {
				tasks.Add(staff.TakingTo(c3));
			}
			
			while (tasks.Count > 0)
            {
				Task< IClient> finishedTask = await Task.WhenAny(tasks);
				
				finishedTask.Result.Receive();

				tasks.Remove(finishedTask);
			}
		}
	}
	public class Client : IClient
	{
        readonly Order order = new();
		ICoffee coffee;
		public void RememberCoffee(ICoffee coffee)
        {
			this.coffee = coffee;
		}
		public Order GetOrder()
        {
			return order;
		}
		public Client(Constanst.Menu menu, Constanst.CupSize cupSize)
        {
			order.Menu = menu;
			order.CupSize = cupSize;
        }
		public void Receive()
		{
			if (coffee == null) return;
			Console.WriteLine("Let check order: " + coffee.Ready().Display());
		}
	}
	public class Staff
	{
        Order order;
		ICoffeeFactory factory; 
		ICoffee coffee;
		public ICoffee Coffee { get { return coffee; } }
		public Staff(Order order)
        {
			GetOrder(order);
		}
		public void GetOrder(Order order)
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
					if(factory == null || factory.GetType() != typeof(ICoffeeFactory))
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
					coffee = factory.CreateBlackCoffeeIce(order.CupSize); 
					break;
				case Constanst.Menu.MilkCoffeeHot:
				case Constanst.Menu.MilkCoffeeIce:
					
					break;
			}
			bool hasResource = coffee != null && coffee.HasResource();
			if(!hasResource)
				Console.WriteLine($"Sorry, has not {ExtensionMethod.GetStringValue(order.Menu)} with {order.CupSize} at the moment");
			return hasResource;
		}
		public async Task<IClient> TakingTo(IClient client)
        {
			client.RememberCoffee(coffee);
			await coffee.Make(); // making
			return client;
		}
	}
}
