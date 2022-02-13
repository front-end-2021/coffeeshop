using System;
using CoffeeShop.GlobalConstant;

namespace CoffeeShop.DesignPattern
{
	public class Client
	{
		bool hasWait = false;
		public ICoffeeFactory OrderWhiteCoffee() { return new CoffeeFactory(); }
		public void Wait(bool hasResouce)
		{
			if (!hasResouce) Console.WriteLine("Sorry, has not at the moment");
			else hasWait = hasResouce;
		}
		public void Receive(ICoffeeDisplay coffee)
		{
			if (!hasWait || coffee == null) return;
			Console.WriteLine("Check order: " + coffee.Display());
		}
	}
	public class Staff
	{
        readonly ICoffeeFactory factory; ICoffee coffee;
		public Staff(ICoffeeFactory factory) { this.factory = factory; }
		public bool HasWhiteCoffee(bool isHot, Constanst.CupSize size)
		{
			if (!isHot) coffee = factory.CreateWhiteCoffeeIce(size);
			else coffee = factory.CreateWhiteCoffeeHot(size);
			return coffee.HasResource();
		}
		public ICoffeeDisplay TakeCoffeeBack()
		{
			coffee.Make();      // waiting
			return coffee.Ready();
		}
	}
	public abstract class Builder : ICoffeeDisplay
	{
		protected int filterCoffee;
		protected Constanst.CupSize cupSize = Constanst.CupSize.Small;
		protected bool hasResource;
		
		public virtual string Display() { 
			return cupSize.ToString(); 
		}
	}
	public class WhiteCoffee : Builder, ICoffee
	{
		protected int milk;
		public WhiteCoffee(Constanst.CupSize cupSize) { 
			this.cupSize = cupSize;

			bool hasFilterCoffee = CoffeeRawMaterials.Self.CheckFilterCoffee(cupSize);
			bool hasMilk = CoffeeRawMaterials.Self.CheckMilk(cupSize);
			hasResource = hasFilterCoffee && hasMilk;
		}
		public virtual bool HasResource()
		{
			return hasResource;
		}
		public virtual /*async*/ void Make()
		{
			if(hasResource)
            {
				filterCoffee = CoffeeRawMaterials.Self.GetFilterCoffee(cupSize);
				milk = CoffeeRawMaterials.Self.GetMilk(cupSize);
			}
			//hasResource = false;
		}
		public virtual ICoffeeDisplay Ready() {
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk;
			if (Global.GetValueFromCupSize(cupSize, size) == (filterCoffee + milk)) 
				return this; 
			return null; 
		}
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
		}
		
		public override void Make()
		{
			base.Make();
			if (hasResource) 
				iceBlend = CoffeeRawMaterials.Self.GetIceBlend(cupSize);
			hasResource = false;
		}
		public override ICoffeeDisplay Ready() {
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk + (int)Constanst.ValMid.IceBlend;
			if (Global.GetValueFromCupSize(cupSize, size) == (filterCoffee + milk + iceBlend)) 
				return this; 
			return null; 
		}
		public override string Display()
		{
			return string.Format("{0}, has ice", base.Display());
		}
	}
	public class WhiteCoffeeHot : WhiteCoffee
	{
		int boldedW;
		public WhiteCoffeeHot(Constanst.CupSize size) : base(size) {
			bool hasBoildWater = CoffeeRawMaterials.Self.CheckBoiledWater(cupSize);
			hasResource = hasResource && hasBoildWater;
		}		
		public override void Make()
		{
			base.Make();
			if (hasResource)
				boldedW = CoffeeRawMaterials.Self.GetBoiledWater(cupSize);
			hasResource = false;
		}
		public override ICoffeeDisplay Ready()
		{
			int size = (int)Constanst.ValMid.FilterCoffee + (int)Constanst.ValMid.Milk + (int)Constanst.ValMid.BoiledWater;
			if (Global.GetValueFromCupSize(cupSize, size) == (filterCoffee + milk + boldedW))
				return this;
			return null;
		}
		public override string Display() {
			return string.Format("{0}, hot", base.Display()); 
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
	}

}
