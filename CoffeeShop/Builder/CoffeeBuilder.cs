using CoffeeShop.GlobalConstant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
    public class Director
    {
        private ICoffeeBuilder builder;
        public ICoffeeBuilder Builder
        {
            set { builder = value; }
        }

        public int BuildBlackCoffeeHot(Constanst.CupSize cupSize)
        {
            return builder.TakeFilterCoffee(cupSize) + 
                   builder.TakeBoiledWater(cupSize);
        }
        public int BuildBlackCoffeeIce(Constanst.CupSize cupSize)
        {
            return builder.TakeFilterCoffee(cupSize) + 
                   builder.TakeIceBlend(cupSize);
        }
        public int BuildWhiteCoffeeIce(Constanst.CupSize cupSize)
        {
            return builder.TakeFilterCoffee(cupSize) + 
                   builder.TakeMilk(cupSize) + 
                   builder.TakeIceBlend(cupSize);
        }
        public int BuildWhiteCoffeeHot(Constanst.CupSize cupSize)
        {
            return builder.TakeFilterCoffee(cupSize) +
                   builder.TakeMilk(cupSize) +
                   builder.TakeBoiledWater(cupSize);
        }        
    }
    public class CoffeeBuilder : ICoffeeBuilder
    {
        private CupCoffee product = new();
        public CoffeeBuilder()
        {
            Reset();
        }
        public void Reset()
        {
            product = new();
        }
        public CupCoffee GetCoffee()
        {
            CupCoffee result = this.product;
            Reset();
            return result;
        }
        public int TakeBoiledWater(Constanst.CupSize cupSize)
        {
            product.AddMaterial(ExtensionMethod.TakeBoiledWater(cupSize));
            return 1000; // 1 sec
        }
        public int TakeCondensedMilk(Constanst.CupSize cupSize)
        {
            product.AddMaterial(ExtensionMethod.TakeCondensedMilk(cupSize));
            return 6000;    // 6 secs
        }
        public int TakeFilterCoffee(Constanst.CupSize cupSize)
        {
            product.AddMaterial(ExtensionMethod.TakeFilterCoffee(cupSize));
            return 3000;    // 3 secs
        }
        public int TakeIceBlend(Constanst.CupSize cupSize)
        {
            product.AddMaterial(ExtensionMethod.TakeIceBlend(cupSize));
            return 5000; // 5 secs
        }
        public int TakeMilk(Constanst.CupSize cupSize)
        {
            product.AddMaterial(ExtensionMethod.TakeMilk(cupSize));
            return 2000; // 2 secs
        }
    }
    public class CupCoffee
    {
        private readonly List<Task<int>> materials = new();
        public void AddMaterial(Task<int> takingMat)
        {
            materials.Add(takingMat);
        }
        public async Task<int> Taking()
        {
            int sumM = 0;
            while (materials.Count > 0)
            {
                Task<int> finishedTask = await Task.WhenAny(materials);
                sumM += finishedTask.Result;
                materials.Remove(finishedTask);
            }
            return sumM;
        }
    }
}
