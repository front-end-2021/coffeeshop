using CoffeeShop.GlobalConstant;
using System;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
	/// <summary>
	/// CoffeeFactory class is Abstract Factory
	/// </summary>
	public class CoffeeFactory : ICoffeeFactory
	{
        readonly Director director = new();
        readonly CoffeeBuilder builder = new();

		public CoffeeFactory()
        {
			director.Builder = builder;
		}
		public ICoffee CreateWhiteCoffeeIce(Constanst.CupSize size)
		{
			return new WhiteCoffeeIce(size);
		}
		public ICoffee CreateWhiteCoffeeHot(Constanst.CupSize size)
		{
			return new WhiteCoffeeHot(size);
		}
		public ICoffee CreateBlackCoffeeIce(Constanst.CupSize size)
		{
			return new BlackCoffeeIce(size);
		}
        public ICoffee CreateBlackCoffeeHot(Constanst.CupSize size)
        {
            return new BlackCoffeeHot(size);
        }

        public Director GetDirector()
        {
			return director;
		}
        public CoffeeBuilder GetBuilder()
        {
			return builder;
		}
    }
	public class WhiteCoffee : BlackCoffee
	{
		protected int milk;
		public WhiteCoffee(Constanst.CupSize cupSize) : base(cupSize)
		{
			bool hasMilk = CoffeeRawMaterials.Self.CheckMilk(cupSize);
			hasResource = hasResource && hasMilk;
		}
		public override string Display()
		{
			return string.Format("WhiteCoffee: size {0}", cupSize.ToString());
		}
	}
	public class WhiteCoffeeIce : WhiteCoffee
	{
		public WhiteCoffeeIce(Constanst.CupSize cupSize) : base(cupSize) {
			bool hasIce = CoffeeRawMaterials.Self.CheckIceBlend(cupSize);
			hasResource = hasResource && hasIce;
		}
		public override async Task Make(Director director, CoffeeBuilder builder)
		{
			if (hasResource)
            {
				int tWait = director.BuildWhiteCoffeeIce(cupSize);
				Console.WriteLine($"Making WhiteCoffeeIce size {cupSize} in {tWait}");
				sumM = await builder.GetCoffee().Taking();
			}
			hasResource = false;        // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready() {
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk + (int)Constanst.ValMid.IceBlend;
			if (Global.GetValueFromCupSize(cupSize, size) == sumM) 
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
		public WhiteCoffeeHot(Constanst.CupSize size) : base(size) {
			bool hasBoildWater = CoffeeRawMaterials.Self.CheckBoiledWater(cupSize);
			hasResource = hasResource && hasBoildWater;
		}		
		public override async Task Make(Director director, CoffeeBuilder builder)
		{
			if (hasResource)
			{
				int tWait = director.BuildWhiteCoffeeHot(cupSize);
				Console.WriteLine($"Making WhiteCoffeeHot size {cupSize} in {tWait}"); 
				sumM = await builder.GetCoffee().Taking();
			}
			hasResource = false;        // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready()
		{
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk + (int)Constanst.ValMid.BoiledWater;
			if (Global.GetValueFromCupSize(cupSize, size) == sumM)
				return this;
			return null;
		}
		public override string Display() {
			return string.Format("{0}, HOT", base.Display());
		}
	}
}
