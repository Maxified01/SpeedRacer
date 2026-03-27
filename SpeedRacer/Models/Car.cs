using System;

namespace SpeedRacer.Models
{
    public class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int Acceleration { get; set; }
        public int MaxFuel { get; set; }
        public int CurrentFuel { get; set; }

        public Car(string name, int speed, int acceleration, int maxFuel)
        {
            Name = name;
            Speed = speed;
            Acceleration = acceleration;
            MaxFuel = maxFuel;
            CurrentFuel = maxFuel;
        }

        public void ConsumeFuel(int amount)
        {
            CurrentFuel -= amount;
            if (CurrentFuel < 0) CurrentFuel = 0;
        }

        public void Refuel()
        {
            CurrentFuel = MaxFuel;
        }
    }
}