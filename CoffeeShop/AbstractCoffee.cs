using System;
using CoffeeShop.GlobalConstant;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
	public class BlackCoffee : TheCoffee, ICoffee
	{
		public BlackCoffee(Constanst.CupSize cupSize)
		{
			this.cupSize = cupSize;

			bool hasFilterCoffee = CoffeeRawMaterials.Self.CheckFilterCoffee(cupSize);
			hasResource = hasFilterCoffee;
		}
		public virtual async Task Make(Director director, CoffeeBuilder builder) {
			await Task.Delay(100);
		}
		public virtual ICoffeeDisplay Ready() { return this; }
		public override string Display()
		{
			return string.Format("BlackCoffee: size {0}", base.Display());
		}
	}
	public class BlackCoffeeIce : BlackCoffee
	{
		public BlackCoffeeIce(Constanst.CupSize cupSize) : base(cupSize)
		{
			bool hasIce = CoffeeRawMaterials.Self.CheckIceBlend(cupSize);
			hasResource = hasResource && hasIce;
		}
		public override async Task Make(Director director, CoffeeBuilder builder)
		{
			if (hasResource)
			{
				int tWait = director.BuildBlackCoffeeIce(cupSize);
				Console.WriteLine($"Making BlackCoffeeIce size {cupSize} in {tWait}");
				sumM = await builder.GetCoffee().Taking();
			}
			hasResource = false; // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready()
		{
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.IceBlend;
			if (Global.GetValueFromCupSize(cupSize, size) == sumM)
				return this;
			return null;
		}
		public override string Display()
		{
			return string.Format("{0} with ICE", base.Display());
		}
	}
	public class BlackCoffeeHot : BlackCoffee
	{
		public BlackCoffeeHot(Constanst.CupSize cupSize) : base(cupSize)
		{
			bool hasBoiledW = CoffeeRawMaterials.Self.CheckBoiledWater(cupSize);
			hasResource = hasResource && hasBoiledW;
		}
		public override async Task Make(Director director, CoffeeBuilder builder)
		{
			if (hasResource)
			{
				int tWait = director.BuildBlackCoffeeHot(cupSize);
				Console.WriteLine($"Making BlackCoffeeHot size {cupSize} in {tWait}");
				sumM = await builder.GetCoffee().Taking();
			}
			hasResource = false; // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready()
		{
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.BoiledWater;
			if (Global.GetValueFromCupSize(cupSize, size) == sumM)
				return this;
			return null;
		}
		public override string Display() {
			return string.Format("{0}, HOT", base.Display());
		}
	}
	public abstract class TheCoffee : ICoffeeDisplay
	{
		protected Constanst.CupSize cupSize = Constanst.CupSize.Medium;
		protected bool hasResource;
		protected int sumM = 0;
		public virtual bool HasResource()
		{
			return hasResource;
		}
		public virtual string Display()
		{
			return cupSize.ToString();
		}
	}
}
