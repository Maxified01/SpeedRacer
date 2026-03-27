using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedRacer.Models;
using SpeedRacer.Managers;
using SpeedRacer.Enums;

namespace SpeedRacer.Tests
{
    [TestClass]
    public class SpeedRacerTests
    {
        [TestMethod]
        public void SpeedUp_ConsumesFuelAndTime()
        {
            // Arrange
            var car = new Car("Speedster", 10, 5, 100);
            var race = new RaceManager(car);

            // Act
            race.TakeTurn(GameAction.SpeedUp);

            // Assert
            Assert.AreEqual(95, car.CurrentFuel, "Fuel should decrease by fuel consumption.");
            Assert.AreEqual(90, race.TimeRemaining, "Time should decrease by 10 units.");
            Assert.AreEqual(2, race.CurrentLap, "Lap should increment by 1.");
        }

        [TestMethod]
        public void MaintainSpeed_ConsumesLessTime()
        {
            var car = new Car("EcoCar", 7, 3, 100);
            var race = new RaceManager(car);

            race.TakeTurn(GameAction.Maintain);

            Assert.AreEqual(97, car.CurrentFuel);
            Assert.AreEqual(93, race.TimeRemaining);
            Assert.AreEqual(2, race.CurrentLap);
        }

        [TestMethod]
        public void PitStop_RefuelsCar()
        {
            var car = new Car("Turbo", 12, 8, 100);
            car.CurrentFuel = 20;
            var race = new RaceManager(car);

            race.TakeTurn(GameAction.PitStop);

            Assert.AreEqual(100, car.CurrentFuel, "Pit stop should fully refuel car.");
            Assert.AreEqual(95, race.TimeRemaining, "Pit stop consumes 5 units of time.");
            Assert.AreEqual(2, race.CurrentLap);
        }

        [TestMethod]
        public void RaceEnds_WhenFuelDepleted()
        {
            var car = new Car("Speedster", 10, 50, 40);
            var race = new RaceManager(car);

            race.TakeTurn(GameAction.SpeedUp);

            Assert.IsTrue(race.IsRaceOver, "Race should end if fuel reaches 0.");
            Assert.AreEqual(0, car.CurrentFuel);
        }

        [TestMethod]
        public void RaceEnds_WhenTimeDepleted()
        {
            var car = new Car("EcoCar", 7, 3, 100);
            var race = new RaceManager(car);

            race.TakeTurn(GameAction.SpeedUp); // time = 90
            race.TakeTurn(GameAction.SpeedUp); // time = 80
            race.TakeTurn(GameAction.SpeedUp); // time = 70
            race.TakeTurn(GameAction.SpeedUp); // time = 60
            race.TakeTurn(GameAction.SpeedUp); // time = 50
            race.TakeTurn(GameAction.SpeedUp); // time = 40
            race.TakeTurn(GameAction.SpeedUp); // time = 30
            race.TakeTurn(GameAction.SpeedUp); // time = 20
            race.TakeTurn(GameAction.SpeedUp); // time = 10
            race.TakeTurn(GameAction.SpeedUp); // time = 0 → race over

            Assert.IsTrue(race.IsRaceOver, "Race should end if time reaches 0.");
            Assert.AreEqual(0, race.TimeRemaining);
        }

        [TestMethod]
        public void LapDoesNotExceedTotalLaps()
        {
            var car = new Car("Turbo", 12, 8, 100);
            var race = new RaceManager(car);

            // Complete all laps
            for (int i = 0; i < 10; i++)
                race.TakeTurn(GameAction.Maintain);

            Assert.AreEqual(race.TotalLaps, race.CurrentLap, "Current lap should not exceed total laps.");
            Assert.IsTrue(race.IsRaceOver, "Race should end after reaching total laps.");
        }
    }
}