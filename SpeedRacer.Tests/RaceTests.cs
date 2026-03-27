using SpeedRacer.Enums;
using SpeedRacer.Managers;
using SpeedRacer.Models;
using System.Runtime.ConstrainedExecution;
using Xunit;

public class RaceTests
{
    [Fact]
    public void Fuel_Decreases_When_SpeedUp()
    {
        var car = new Car("Test", 10, 5, 100);
        var race = new RaceManager(car);

        race.TakeTurn(GameAction.SpeedUp);

        Assert.True(car.CurrentFuel < 100);
    }

    [Fact]
    public void Lap_Increases_When_Progress_Reaches_100()
    {
        var car = new Car("Test", 100, 1, 100);
        var race = new RaceManager(car);

        race.TakeTurn(GameAction.Maintain);

        Assert.Equal(2, race.CurrentLap);
    }

    [Fact]
    public void Game_Ends_When_Fuel_Zero()
    {
        var car = new Car("Test", 10, 100, 100);
        var race = new RaceManager(car);

        race.TakeTurn(GameAction.Maintain);

        Assert.True(race.IsRaceOver);
    }
}