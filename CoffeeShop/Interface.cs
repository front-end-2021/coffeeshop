using CoffeeShop.GlobalConstant;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
	public class Order
    {
		public Constanst.Menu Menu { get; set; } = Constanst.Menu.BlackCoffeeHot;
		public Constanst.CupSize CupSize { get; set; } = Constanst.CupSize.Medium;
	}
	public interface IClient
    {
		Order GetOrder();
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
	}
}
