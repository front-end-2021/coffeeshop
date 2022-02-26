using CoffeeShop.GlobalConstant;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
	public interface ICoffeeBuilder
	{
		int TakeFilterCoffee(Constanst.CupSize cupSize);
		int TakeMilk (Constanst.CupSize cupSize);
		int TakeIceBlend(Constanst.CupSize cupSize);
		int TakeBoiledWater(Constanst.CupSize cupSize);
		int TakeCondensedMilk(Constanst.CupSize cupSize);
	}
	public interface IClient
    {
		void RememberCoffee(ICoffee coffee);
		string Receive();
	}
	public interface ICoffeeDisplay
	{
		string Display();
	}
	public interface ICoffee
	{
		Task Make(Director director, CoffeeBuilder builder);
		ICoffeeDisplay Ready();
		bool HasResource();
	}
	public interface ICoffeeFactory
	{
		Director GetDirector();
		CoffeeBuilder GetBuilder();
		ICoffee CreateWhiteCoffeeIce(Constanst.CupSize size);
		ICoffee CreateWhiteCoffeeHot(Constanst.CupSize size);
		ICoffee CreateBlackCoffeeIce(Constanst.CupSize size);
		ICoffee CreateBlackCoffeeHot(Constanst.CupSize size);
	}
}
