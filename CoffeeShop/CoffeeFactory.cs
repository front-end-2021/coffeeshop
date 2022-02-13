using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Staff(ICoffeeFactory f) { this.factory = f; }
		public bool HasWhiteCoffee(bool isHot, Constanst.CupSize size)
		{
			if (!isHot) coffee = this.factory.CreateWhiteCoffeeIce(size);
			else coffee = this.factory.CreateWhiteCoffeeHot(size);
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
		protected long size;
		protected Constanst.CupSize cupSize = Constanst.CupSize.Small;
		protected int GetFilterCoffee() { return 30; }
		public virtual string Display() { return cupSize.ToString(); }
	}
	public interface ICoffeeDisplay { 
		string Display(); 
	}
	public interface ICoffee { 
		void Make(); 
		ICoffeeDisplay Ready(); 
		bool HasResource(); }
	public class WhiteCoffee : Builder, ICoffee
	{
		protected int milk;
		public WhiteCoffee(Constanst.CupSize size) { this.cupSize = size; }
		protected int GetMilk() { return 100; }
		public virtual bool HasResource()
		{
			bool hasFilterCoffee = true;
			bool hasMilk = true;
			return hasFilterCoffee && hasMilk;
		}
		public virtual void Make()
		{
			this.filterCoffee = GetFilterCoffee();
			this.milk = GetMilk();
		}
		public virtual ICoffeeDisplay Ready() { 
			if (this.size == 380) 
				return this; 
			return null; }//defaultWhiteCoffeeHot
		public override string Display()
		{
			return string.Format("WhiteCoffee: size {0}, has milk", base.Display());
		}
	}
	public class WhiteCoffeeIce : WhiteCoffee
	{
		int iceBlend;
		public WhiteCoffeeIce(Constanst.CupSize size) : base(size) { }
		private int GetIceBlend() { return 500; }
		public override bool HasResource()
		{
			bool a = base.HasResource();
			bool hasIce = true;
			return a && hasIce;
		}
		public override void Make()
		{
			base.Make();
			iceBlend = GetIceBlend();
			size = filterCoffee + milk + iceBlend;
		}
		public override ICoffeeDisplay Ready() { 
			if (this.size == 630) return this; return null; 
		}
		public override string Display()
		{
			return string.Format("{0}, has ice", base.Display());
		}
	}
	public class WhiteCoffeeHot : WhiteCoffee
	{
		int boldedW;
		public WhiteCoffeeHot(Constanst.CupSize size) : base(size) { }
		private int GetBoiledWater() { return 250; }
		public override bool HasResource()
		{
			bool a = base.HasResource();
			bool hasBolded = true;
			return a && hasBolded;
		}
		public override void Make()
		{
			base.Make();
			boldedW = GetBoiledWater();
			size = filterCoffee + milk + boldedW;
		}
		public override string Display() { 
			return string.Format("{0}, hot", base.Display()); 
		}
	}
	public interface ICoffeeFactory { 
		ICoffee CreateWhiteCoffeeIce(Constanst.CupSize size); 
		ICoffee CreateWhiteCoffeeHot(Constanst.CupSize size); 
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
