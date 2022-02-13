using CoffeeShop.GlobalConstant;
using System;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
	public class WhiteCoffee : BlackCoffee, ICoffee
	{
		protected int milk;
		public WhiteCoffee(Constanst.CupSize cupSize) : base(cupSize)
		{
			bool hasMilk = CoffeeRawMaterials.Self.CheckMilk(cupSize);
			hasResource = hasResource && hasMilk;
		}
		protected void TakeCoffeeAndMilk()
        {
			TakeFilterCoffee();
			milk = CoffeeRawMaterials.Self.GetMilk(cupSize);
		}
		public override string Display()
		{
			return string.Format("WhiteCoffee: size {0}", base.Display());
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
				Console.WriteLine($"Making WhiteCoffeeIce size {cupSize} in {timeToWait}");
				await Task.Delay(timeToWait);
				TakeCoffeeAndMilk();
				iceBlend = CoffeeRawMaterials.Self.GetIceBlend(cupSize);
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
				Console.WriteLine($"Making WhiteCoffeeHot size {cupSize} in {timeToWait}");
				await Task.Delay(timeToWait);
				TakeCoffeeAndMilk();
				boldedW = CoffeeRawMaterials.Self.GetBoiledWater(cupSize);
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
        public ICoffee CreateBlackCoffeeIce(Constanst.CupSize size)
        {
            return new BlackCoffeeIce(size);
        }
        //public ICoffee CreateBlackCoffeeHot(Constanst.CupSize size) {
        //	return new BlackCoffeeHot(size);
        //}
    }

}
