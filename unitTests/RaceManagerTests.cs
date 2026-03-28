using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedRacer.Managers;
using SpeedRacer.Models;
using SpeedRacer.Enums;

namespace SpeedRacer.UnitTests
{
    [TestClass]
    public class RaceManagerTests
    {
        private Car car;
        private RaceManager manager;

        [TestInitialize]
        public void Setup()
        {
            car = new Car("TestCar", 10, 5, 100);
            manager = new RaceManager(car);
        }

        [TestMethod]
        public void SpeedUp_ConsumesFuel()
        {
            manager.TakeTurn(GameAction.SpeedUp);
            Assert.AreEqual(95, car.CurrentFuel);
        }

        [TestMethod]
        public void Maintain_ConsumesLessFuel()
        {
            manager.TakeTurn(GameAction.Maintain);
            Assert.AreEqual(98, car.CurrentFuel);
        }

        [TestMethod]
        public void PitStop_RefillsFuel()
        {
            car.CurrentFuel = 50;
            manager.TakeTurn(GameAction.PitStop);
            Assert.AreEqual(100, car.CurrentFuel);
        }

        [TestMethod]
        public void Lap_Increases_After_Action()
        {
            manager.TakeTurn(GameAction.Maintain);
            Assert.AreEqual(1, manager.CurrentLap);
        }

        [TestMethod]
        public void Race_Ends_When_Laps_Complete()
        {
            for (int i = 0; i < manager.TotalLaps; i++)
            {
                manager.TakeTurn(GameAction.Maintain);
            }

            Assert.IsTrue(manager.IsRaceFinished);
        }

        [TestMethod]
        public void Fuel_Never_Goes_Negative()
        {
            car.CurrentFuel = 1;
            manager.TakeTurn(GameAction.SpeedUp);

            Assert.IsTrue(car.CurrentFuel >= 0);
        }

        [TestMethod]
        public void Cannot_SpeedUp_When_No_Fuel()
        {
            car.CurrentFuel = 0;
            manager.TakeTurn(GameAction.SpeedUp);

            Assert.AreEqual(0, car.CurrentFuel);
        }
    }
}