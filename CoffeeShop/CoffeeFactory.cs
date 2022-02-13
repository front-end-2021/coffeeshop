using CoffeeShop.GlobalConstant;
using System;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{	
	public abstract class TheCoffee : ICoffeeDisplay
	{
		protected int timeToWait = 1000;
		protected int filterCoffee;
		protected Constanst.CupSize cupSize = Constanst.CupSize.Medium;
		protected bool hasResource;
		public virtual bool HasResource()
		{
			return hasResource;
		}
		public virtual string Display() { 
			return cupSize.ToString(); 
		}
	}
	public class WhiteCoffee : TheCoffee, ICoffee
	{
		protected int milk;
		public WhiteCoffee(Constanst.CupSize cupSize) { 
			this.cupSize = cupSize;

			bool hasFilterCoffee = CoffeeRawMaterials.Self.CheckFilterCoffee(cupSize);
			bool hasMilk = CoffeeRawMaterials.Self.CheckMilk(cupSize);
			hasResource = hasFilterCoffee && hasMilk;
		}
		public virtual async Task Make() {}
		protected void TakeCoffeeAndMilk()
        {
			filterCoffee = CoffeeRawMaterials.Self.GetFilterCoffee(cupSize);
			milk = CoffeeRawMaterials.Self.GetMilk(cupSize);
		}
		public virtual ICoffeeDisplay Ready() { return this; }
		public override string Display()
		{
			return string.Format("WhiteCoffee: size {0}, has milk", base.Display());
		}
	}
	public class WhiteCoffeeIce : WhiteCoffee
	{
		int iceBlend;
		public WhiteCoffeeIce(Constanst.CupSize cupSize) : base(cupSize) {
			bool hasIce = CoffeeRawMaterials.Self.CheckIceBlend(cupSize);
			hasResource = hasResource && hasIce;
			timeToWait = 3000;
		}
		public override async Task Make()
		{
			if (hasResource)
            {
				await Task.Delay(timeToWait);
				TakeCoffeeAndMilk();
				iceBlend = CoffeeRawMaterials.Self.GetIceBlend(cupSize);
				Console.WriteLine($"Making WhiteCoffeeIce size {cupSize} in {timeToWait}");
			}
			hasResource = false;        // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready() {
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk + (int)Constanst.ValMid.IceBlend;
			if (Global.GetValueFromCupSize(cupSize, size) == (filterCoffee + milk + iceBlend)) 
				return this; 
			return null; 
		}
		public override string Display()
		{
			return string.Format("{0} with ICE", base.Display());
		}
	}
	public class WhiteCoffeeHot : WhiteCoffee
	{
		int boldedW;
		public WhiteCoffeeHot(Constanst.CupSize size) : base(size) {
			bool hasBoildWater = CoffeeRawMaterials.Self.CheckBoiledWater(cupSize);
			hasResource = hasResource && hasBoildWater;
		}		
		public override async Task Make()
		{
			if (hasResource)
            {
				await Task.Delay(timeToWait);
				TakeCoffeeAndMilk();
				boldedW = CoffeeRawMaterials.Self.GetBoiledWater(cupSize);
				Console.WriteLine($"Making WhiteCoffeeHot size {cupSize} in {timeToWait}");
			}
			hasResource = false;        // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready()
		{
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk + (int)Constanst.ValMid.BoiledWater;
			if (Global.GetValueFromCupSize(cupSize, size) == (filterCoffee + milk + boldedW))
				return this;
			return null;
		}
		public override string Display() {
			return string.Format("{0}, HOT", base.Display());
		}
	}
	
	public class CoffeeFactory : ICoffeeFactory
	{
		public ICoffee CreateWhiteCoffeeIce(Constanst.CupSize size) {
			return new WhiteCoffeeIce(size);
		}
		public ICoffee CreateWhiteCoffeeHot(Constanst.CupSize size) {
			return new WhiteCoffeeHot(size);
		}
		//public ICoffee CreateBlackCoffeeIce(Constanst.CupSize size) {
		//	return new BlackCoffeeIce(size);
		//}
		//public ICoffee CreateBlackCoffeeHot(Constanst.CupSize size) {
		//	return new BlackCoffeeHot(size);
		//}
	}

}
