using System;
using CoffeeShop.GlobalConstant;
using CoffeeShop.DesignPattern;
using System.Threading.Tasks;

namespace CoffeeShop
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine(Global.GetMenu());


			ClientCode(new ClientCreator(Constanst.Menu.WhiteCoffeeHot, Constanst.CupSize.Small));
			ClientCode(new ClientCreator(Constanst.Menu.WhiteCoffeeIce, Constanst.CupSize.Medium));
			ClientCode(new ClientCreator(Constanst.Menu.BlackCoffeeIce, Constanst.CupSize.Small));
			ClientCode(new ClientCreator(Constanst.Menu.BlackCoffeeHot, Constanst.CupSize.Large));

            while (Global.Tasks.Count > 0)
            {
                Task<IClient> finishedTask = await Task.WhenAny(Global.Tasks);
                finishedTask.Result.Receive();
                Global.Tasks.Remove(finishedTask);
            }
        }
		static void ClientCode(Creator creator)
		{
			creator.ProcessingOrder(); 
		}
	}
	
	public class Staff
	{
        readonly Constanst.Menu menu;
        readonly ICoffeeFactory factory; 
		ICoffee coffee;
		
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
					if(factory == null || factory.GetType() != typeof(ICoffeeFactory))
						factory = new CoffeeFactory();
					break;
			}
		}
		public bool HasResource(Constanst.CupSize cupSize)
		{
			if (factory == null) return false;
			switch (menu)
			{
				case Constanst.Menu.WhiteCoffeeHot:
					coffee = factory.CreateWhiteCoffeeHot(cupSize);
					break;
				case Constanst.Menu.WhiteCoffeeIce:
					coffee = factory.CreateWhiteCoffeeIce(cupSize);
					break;
				case Constanst.Menu.BlackCoffeeHot:
					coffee = factory.CreateBlackCoffeeHot(cupSize);
					break;
				case Constanst.Menu.BlackCoffeeIce:
					coffee = factory.CreateBlackCoffeeIce(cupSize);
					break;
				case Constanst.Menu.MilkCoffeeHot:
				case Constanst.Menu.MilkCoffeeIce:
					
					break;
			}
			bool hasResource = coffee != null && coffee.HasResource();
			if(!hasResource)
				Console.WriteLine($"Sorry, has not {ExtensionMethod.GetStringValue(menu)} with {cupSize} at the moment");
			return hasResource;
		}
		public async Task<IClient> TakingTo(IClient client)
        {
			client.RememberCoffee(coffee);			
			await coffee.Make(factory.GetDirector(), factory.GetBuilder()); // making
			return client;
		}
	}
}
