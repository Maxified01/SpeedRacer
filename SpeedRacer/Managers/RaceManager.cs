using SpeedRacer.Models;
using SpeedRacer.Enums;
using System;

namespace SpeedRacer.Managers
{
    public class RaceManager
    {
        public Car PlayerCar { get; private set; }
        public int CurrentLap { get; private set; } = 0;
        public int TotalLaps { get; private set; } = 5;
        public int TimeRemaining { get; private set; } = 60;
        public bool IsRaceOver { get; private set; } = false;
        public bool PlayerWon { get; private set; } = false;

        public RaceManager(Car playerCar)
        {
            PlayerCar = playerCar;
        }

        // Take turns
        public void TakeTurn(GameAction action)
        {
            if (IsRaceOver) return;

            switch (action)
            {
                case GameAction.SpeedUp:
                    PlayerCar.CurrentFuel -= 5;
                    CurrentLap++;
                    break;
                case GameAction.Maintain:
                    PlayerCar.CurrentFuel -= 2;
                    CurrentLap++;
                    break;
                case GameAction.PitStop:
                    PlayerCar.CurrentFuel = PlayerCar.MaxFuel;
                    CurrentLap++;
                    break;
            }

            TimeRemaining--;
            if (CurrentLap >= TotalLaps || PlayerCar.CurrentFuel <= 0 || TimeRemaining <= 0)
            {
                IsRaceOver = true;
                PlayerWon = PlayerCar.CurrentFuel > 0 && CurrentLap >= TotalLaps;
            }
        }

        // Methods to safely update values
        public void SetCurrentLap(int lap)
        {
            if (lap >= 0 && lap <= TotalLaps) CurrentLap = lap;
        }

        public void SetTimeRemaining(int time)
        {
            if (time >= 0) TimeRemaining = time;
        }

        public void SetPlayerFuel(int fuel)
        {
            PlayerCar.CurrentFuel = fuel;
        }

        internal void SetPlayerSpeed(int v)
        {
            throw new NotImplementedException();
        }
    }
}