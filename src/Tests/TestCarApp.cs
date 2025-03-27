using CarApp.Model;
using CarApp.Type;

namespace TestCarApp
{
    [TestClass]
    public sealed class TestCarApp
    {
        [TestMethod]
        public void TestCarAdd()
        {
            Car car = new Car(0, "Toyota", "Corolla", 2022, GearType.Automatic, 15.7, 10000, new Engine("1.8", 142, 90, Engine.FuelType.Benzin, 10000, DateTime.Now, 15000, 6), Wheel.GetSetOf4Wheels(new Tire(brand: "Falken", model: "Wildpeak", width: 225, height: 60, inch: 18, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), "", OwnerList.Instance.GetOwnerById(4));
            Assert.AreEqual("Toyota", car.Brand);
            Assert.AreEqual("Corolla", car.Model);
            Assert.AreEqual(2022, car.Year);
        }
    }
}
