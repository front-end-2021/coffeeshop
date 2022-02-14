using CoffeeShop.GlobalConstant;
using System;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
    public abstract class Creator
    {
        protected Constanst.Menu menu;
        protected Constanst.CupSize cupSize;
        public abstract IClient GetClientFactory();

        public void ProcessingOrder() 
        {
            IClient client = GetClientFactory();    // create Client
            Staff staff = new(menu);
            if (staff.HasResource(cupSize))
            {
                Global.Tasks.Add(staff.TakingTo(client));
            }
        }
    }
    public class ClientCreator : Creator
    {
        public ClientCreator(Constanst.Menu menu, Constanst.CupSize cupSize)
        {
            this.menu = menu; this.cupSize = cupSize;
        }
        public override IClient GetClientFactory()
        {
            return new Client();
        }
    }
    public class Client : IClient
    {
        ICoffee coffee;
        public void RememberCoffee(ICoffee coffee)
        {
            this.coffee = coffee;
        }
       
        public void Receive()
        {
            if (coffee == null) return;
            Console.WriteLine("Let check order: " + coffee.Ready().Display());
        }
    }
}
