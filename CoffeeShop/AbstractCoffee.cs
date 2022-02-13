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
		public virtual async Task Make() { }
		protected void TakeFilterCoffee()
		{
			filterCoffee = CoffeeRawMaterials.Self.GetFilterCoffee(cupSize);
		}
		public virtual ICoffeeDisplay Ready() { return this; }
		public override string Display()
		{
			return string.Format("BlackCoffee: size {0}", base.Display());
		}
	}
	public class BlackCoffeeIce : BlackCoffee
	{
		int iceBlend;
		public BlackCoffeeIce(Constanst.CupSize cupSize) : base(cupSize)
		{
			bool hasIce = CoffeeRawMaterials.Self.CheckIceBlend(cupSize);
			hasResource = hasResource && hasIce;
			timeToWait = 2000;
		}
		public override async Task Make()
		{
			if (hasResource)
			{
				Console.WriteLine($"Making BlackCoffeeIce size {cupSize} in {timeToWait}");
				TakeFilterCoffee();
				await Task.Delay(timeToWait);
				iceBlend = CoffeeRawMaterials.Self.GetIceBlend(cupSize);
			}
			hasResource = false;        // trigger to prevent CoffeeRawMaterials.Self.GetXXX call multiple
		}
		public override ICoffeeDisplay Ready()
		{
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.IceBlend;
			if (Global.GetValueFromCupSize(cupSize, size) == (filterCoffee + iceBlend))
				return this;
			return null;
		}
		public override string Display()
		{
			return string.Format("{0} with ICE", base.Display());
		}
	}
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
		public virtual string Display()
		{
			return cupSize.ToString();
		}
	}
}
