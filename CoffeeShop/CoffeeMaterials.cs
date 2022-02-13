using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.DesignPattern
{
    public class CoffeeMaterials
    {
        private List<Dictionary<byte, long>> source = new List<Dictionary<byte, long>>();



        private static CoffeeMaterials instance;
        public static CoffeeMaterials Self
        {
            get {
                if (instance == null) instance = new CoffeeMaterials();
                return instance;
            }
        }
    }

    public static class Constanst
    {
        public enum CupSize : Byte { Small = 1, Medium = 2, Large = 3 }
        public enum ValMid : ushort { 
            FilterCoffee = 30, // 30ml
            Milk = 120, // 120ml
            IceBlend = 180,   // 180ml
            BoiledWater = 180, // 180ml
            CondensedMilk = 30    // 30ml
        }
        
        public enum Menu : byte
        {
            [StringValue("White Coffee Ice")]
            WhiteCoffeeIce = 1,
            [StringValue("White Coffee Hot")]
            WhiteCoffeeHot = 2,
            [StringValue("Black Coffee Hot")]
            BlackCoffeeHot = 3,
            [StringValue("Black Coffee Ice")]
            BlackCoffeeIce = 4,
            [StringValue("Milk Coffee Ice")]
            MilkCoffeeIce = 5,
            [StringValue("Milk Coffee Hot")]
            MilkCoffeeHot = 6
        }
    }
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
        #endregion
    }
    public static class Global
    {
        public static int GetFilterCoffee(Constanst.CupSize size)
        {
            switch (size)
            {
                case Constanst.CupSize.Small:
                    return (int)Constanst.ValMid.FilterCoffee / 2;
                case Constanst.CupSize.Large:
                    return (int)Constanst.ValMid.FilterCoffee * 2;
                default:
                    return (int)Constanst.ValMid.FilterCoffee;
            }
        }

        public static string GetMenu()
        {
            return string.Format("Welcome The Coffee Shop \n Menu: \n  1. {0}\n  2. {1}\n  3. {2}\n  4. {3}\n  5. {4}\n  6: {5}\n",
                    ExtensionMethod.GetStringValue(Constanst.Menu.WhiteCoffeeIce),
                    ExtensionMethod.GetStringValue(Constanst.Menu.WhiteCoffeeHot),
                    ExtensionMethod.GetStringValue(Constanst.Menu.BlackCoffeeHot),
                    ExtensionMethod.GetStringValue(Constanst.Menu.BlackCoffeeIce),
                    ExtensionMethod.GetStringValue(Constanst.Menu.MilkCoffeeIce),
                    ExtensionMethod.GetStringValue(Constanst.Menu.MilkCoffeeHot));
        }
    }
}
