using System;
using CoffeeShop.GlobalConstant;
using System.Reflection;
using System.Threading.Tasks;
using CoffeeShop.DesignPattern;

namespace CoffeeShop
{
    public static class ExtensionMethod
    {

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static async Task<int> TakeBoiledWater(Constanst.CupSize cupSize)
        {
            Console.Write("Taking Boilded Water 1 sec. ");
            await Task.Delay(1000);
            return CoffeeRawMaterials.Self.GetBoiledWater(cupSize);
        }
        public static async Task<int> TakeCondensedMilk(Constanst.CupSize cupSize)
        {
            Console.Write("Taking Condensed Milk 6 secs. ");
            await Task.Delay(6000);
            return CoffeeRawMaterials.Self.GetcondensedMilk(cupSize);
        }
        public static async Task<int> TakeFilterCoffee(Constanst.CupSize cupSize)
        {
            Console.Write("Taking Filter Coffee 3 secs. ");
            await Task.Delay(3000);
            return CoffeeRawMaterials.Self.GetFilterCoffee(cupSize);
        }
        public static async Task<int> TakeIceBlend(Constanst.CupSize cupSize)
        {
            Console.Write("Taking Ice Blend 5 secs. ");
            await Task.Delay(5000);
            return CoffeeRawMaterials.Self.GetIceBlend(cupSize);
        }
        public static async Task<int> TakeMilk(Constanst.CupSize cupSize)
        {
            Console.Write("Taking Milk 2 secs. ");
            await Task.Delay(2000);
            return CoffeeRawMaterials.Self.GetMilk(cupSize);
        }
    }
}
