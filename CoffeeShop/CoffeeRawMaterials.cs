using CoffeeShop.GlobalConstant;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CoffeeShop.DesignPattern
{
    /// <summary>
    /// Singleton class. Get the Instance by call Self (Properties)
    /// </summary>
    public class CoffeeRawMaterials
    {
        public CoffeeRawMaterials()
        {
            FilterCoffee = 55;
            Milk = 222;
            IceBlend = 222;
            BoiledWater = 222;

            Console.WriteLine($"CoffeeRawMaterials: Filter Coffee = {FilterCoffee}, Milk = {Milk}, Ice Blend = {IceBlend}, Boiled Water = {BoiledWater}");
        }

        private static CoffeeRawMaterials instance;
        private readonly Dictionary<string, int> filterCoffee = new();
        private readonly Dictionary<string, int> filterCoffeeCache = new();

        private int FilterCoffee
        {
            set {
                if (filterCoffee.ContainsKey(Constanst.FilterCoffee))
                    filterCoffee[Constanst.FilterCoffee] = value;
                else
                    filterCoffee.Add(Constanst.FilterCoffee, value);
            }
            get
            {
                if (!filterCoffee.ContainsKey(Constanst.FilterCoffee))
                    filterCoffee.Add(Constanst.FilterCoffee, 0);
                return filterCoffee[Constanst.FilterCoffee];
            }
        }
        private int FilterCoffeeCache
        {
            set
            {
                if (filterCoffeeCache.ContainsKey(Constanst.FilterCoffee))
                    filterCoffeeCache[Constanst.FilterCoffee] = value;
                else
                    filterCoffeeCache.Add(Constanst.FilterCoffee, value);
            }
            get
            {
                if (!filterCoffeeCache.ContainsKey(Constanst.FilterCoffee))
                    filterCoffeeCache.Add(Constanst.FilterCoffee, 0);
                return filterCoffeeCache[Constanst.FilterCoffee];
            }
        }
        public int GetFilterCoffee(Constanst.CupSize cupSize) {
            int value = Global.GetFilterCoffee(cupSize);
            FilterCoffeeCache -= value;
            return value;
        }

        public bool CheckFilterCoffee(Constanst.CupSize cupSize)
        {
            int value = Global.GetFilterCoffee(cupSize);
            if (FilterCoffee >= value)
            {
                FilterCoffee -= value;
                FilterCoffeeCache += value;
                return true;
            }
            Console.WriteLine($"---> Not enough {Constanst.FilterCoffee}: {FilterCoffee}/{value}"); // send message warning!!!
            return false;
        }
        public void BackFilterCoffee(Constanst.CupSize cupSize)
        {
            int value = Global.GetFilterCoffee(cupSize);
            if (FilterCoffeeCache >= value)
            {
                FilterCoffeeCache -= value;
                FilterCoffee += value;
            } else
            {
                Console.WriteLine($"FilterCoffeeCache has {value}");
            }
        }

        private readonly Dictionary<string, int> milk = new();
        private readonly Dictionary<string, int> milkCache = new();
        private int Milk
        {
            set
            {
                if (milk.ContainsKey(Constanst.Milk))
                    milk[Constanst.Milk] = value;
                else
                    milk.Add(Constanst.Milk, value);
            }
            get
            {
                if (!milk.ContainsKey(Constanst.Milk))
                    milk.Add(Constanst.Milk, 0);
                return milk[Constanst.Milk];
            }
        }
        private int MilkCache
        {
            set
            {
                if (milkCache.ContainsKey(Constanst.Milk))
                    milkCache[Constanst.Milk] = value;
                else
                    milkCache.Add(Constanst.Milk, value);
            }
            get
            {
                if (!milkCache.ContainsKey(Constanst.Milk))
                    milkCache.Add(Constanst.Milk, 0);
                return milkCache[Constanst.Milk];
            }
        }
        public int GetMilk(Constanst.CupSize cupSize)
        {
            int value = Global.GetMilk(cupSize);
            MilkCache -= value;
            return value;
        }
        public bool CheckMilk(Constanst.CupSize cupSize)
        {
            int value = Global.GetMilk(cupSize);
            if (Milk >= value)
            {
                Milk -= value;
                MilkCache += value;
                return true;
            }
            Console.WriteLine($"---> Not enough {Constanst.Milk}: {Milk}/{value}"); // send message warning!!!
            return false;
        }
        public void BackMilk(Constanst.CupSize cupSize)
        {
            int value = Global.GetMilk(cupSize);
            if (MilkCache >= value)
            {
                MilkCache -= value;
                Milk += value;
            }
            else
            {
                Console.WriteLine($"MilkCache has {value}");
            }
        }
        
        private readonly Dictionary<string, int> iceBlend = new();
        private readonly Dictionary<string, int> iceBlendCache = new();
        private int IceBlend
        {
            set
            {
                if (iceBlend.ContainsKey(Constanst.IceBlend))
                    iceBlend[Constanst.IceBlend] = value;
                else
                    iceBlend.Add(Constanst.IceBlend, value);
            }
            get
            {
                if (!iceBlend.ContainsKey(Constanst.IceBlend))
                    iceBlend.Add(Constanst.IceBlend, 0);
                return iceBlend[Constanst.IceBlend];
            }
        }
        private int IceBlendCache
        {
            set
            {
                if (iceBlendCache.ContainsKey(Constanst.IceBlend))
                    iceBlendCache[Constanst.IceBlend] = value;
                else
                    iceBlendCache.Add(Constanst.IceBlend, value);
            }
            get
            {
                if (!iceBlendCache.ContainsKey(Constanst.IceBlend))
                    iceBlendCache.Add(Constanst.IceBlend, 0);
                return iceBlendCache[Constanst.IceBlend];
            }
        }
        public int GetIceBlend(Constanst.CupSize cupSize)
        {
            int value = Global.GetIceBlend(cupSize);
            IceBlendCache -= value;
            return value;
        }
        public bool CheckIceBlend(Constanst.CupSize cupSize)
        {
            int value = Global.GetIceBlend(cupSize);
            if (IceBlend >= value)
            {
                IceBlend -= value;
                IceBlendCache += value;
                return true;
            }
            Console.WriteLine($"---> Not enough {Constanst.IceBlend}: {IceBlend}/{value}"); // send message warning!!!
            return false;
        }
        public void BackIceBlend(Constanst.CupSize cupSize)
        {
            int value = Global.GetIceBlend(cupSize);
            if (IceBlendCache >= value)
            {
                IceBlendCache -= value;
                IceBlend += value;
            }
            else
            {
                Console.WriteLine($"MilkCache has {value}");
            }
        }
        
        private readonly Dictionary<string, int> boiledWater = new();
        private readonly Dictionary<string, int> boiledWaterCache = new();
        private int BoiledWater
        {
            set
            {
                if (boiledWater.ContainsKey(Constanst.BoiledWater))
                    boiledWater[Constanst.BoiledWater] = value;
                else
                    boiledWater.Add(Constanst.BoiledWater, value);
            }
            get
            {
                if (!boiledWater.ContainsKey(Constanst.BoiledWater))
                    boiledWater.Add(Constanst.BoiledWater, 0);
                return boiledWater[Constanst.BoiledWater];
            }
        }
        private int BoiledWaterCache
        {
            set
            {
                if (boiledWaterCache.ContainsKey(Constanst.BoiledWater))
                    boiledWaterCache[Constanst.BoiledWater] = value;
                else
                    boiledWaterCache.Add(Constanst.BoiledWater, value);
            }
            get
            {
                if (!boiledWaterCache.ContainsKey(Constanst.BoiledWater))
                    boiledWaterCache.Add(Constanst.BoiledWater, 0);
                return boiledWaterCache[Constanst.BoiledWater];
            }
        }
        public int GetBoiledWater(Constanst.CupSize cupSize)
        {
            int value = Global.GetBoiledWater(cupSize);
            BoiledWaterCache -= value;
            return value;
        }
        public bool CheckBoiledWater(Constanst.CupSize cupSize)
        {
            int value = Global.GetBoiledWater(cupSize);
            if (BoiledWater >= value)
            {
                BoiledWater -= value;
                BoiledWaterCache += value;
                return true;
            }
            Console.WriteLine($"---> Not enough {Constanst.BoiledWater}: {BoiledWater}/{value}"); // send message warning!!!
            return false;
        }
        public void BackBoiledWater(Constanst.CupSize cupSize)
        {
            int value = Global.GetBoiledWater(cupSize);
            if (BoiledWaterCache >= value)
            {
                BoiledWaterCache -= value;
                BoiledWater += value;
            }
            else
            {
                Console.WriteLine($"MilkCache has {value}");
            }
        }

        public static CoffeeRawMaterials Self
        {
            get {
                if (instance == null) instance = new CoffeeRawMaterials();
                return instance;
            }
        }
    }

}
