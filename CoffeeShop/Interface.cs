using CoffeeShop.GlobalConstant;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
	public interface IClient
    {
		void RememberCoffee(ICoffee coffee);
		void Receive();
	}
	public interface ICoffeeDisplay
	{
		string Display();
	}
	public interface ICoffee
	{
		Task Make();
		ICoffeeDisplay Ready();
		bool HasResource();
	}
	public interface ICoffeeFactory
	{
		ICoffee CreateWhiteCoffeeIce(Constanst.CupSize size);
		ICoffee CreateWhiteCoffeeHot(Constanst.CupSize size);
		ICoffee CreateBlackCoffeeIce(Constanst.CupSize size);
	}
}
