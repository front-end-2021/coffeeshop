using CoffeeShop;
using Xunit;
using CoffeeShop.GlobalConstant;
using System.Threading.Tasks;

namespace TestCoffeeShop
{
    public class UnitTestCoffeeShop
    {
        [Fact]
        public async Task TestOrderWhiteCoffeeHotSmallAsync()
        {
            string res = await TheCoffeeShopMall.OrderCoffee(Constanst.Menu.WhiteCoffeeHot, Constanst.CupSize.Small);
            Assert.Contains("WhiteCoffee", res);
            Assert.Contains("HOT", res);
            Assert.Contains("Small", res);
        }
        [Fact]
        public async Task TestOrderBlackCoffeeIceMediumAsync()
        {
            string res = await TheCoffeeShopMall.OrderCoffee(Constanst.Menu.BlackCoffeeIce, Constanst.CupSize.Medium);
            Assert.Contains("BlackCoffee", res);
            Assert.Contains("ICE", res);
            Assert.Contains("Medium", res);
        }
    }
}
