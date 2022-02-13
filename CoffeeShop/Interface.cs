using CoffeeShop.GlobalConstant;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{

	public interface ICoffeeDisplay
	{
		string Display();
	}
	public interface ICoffee
	{
		void Make();
		ICoffeeDisplay Ready();
		bool HasResource();
	}
	public interface ICoffeeFactory
	{
		ICoffee CreateWhiteCoffeeIce(Constanst.CupSize size);
		ICoffee CreateWhiteCoffeeHot(Constanst.CupSize size);
	}
}
