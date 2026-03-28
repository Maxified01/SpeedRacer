# Speed Racer

## Description
**Speed Racer** is a simple racing simulation game built using **C# and WPF**. Players select a car, manage speed and fuel, and complete laps to win the race. The game simulates lap progression, fuel consumption, and race timing, providing visual feedback through animations and progress bars.

### Features:
- Three selectable cars: **Roadstar, Turbocharger, Challenger**
- Animated car movement on the track
- Lap, fuel, and time tracking with progress bars
- Speed Up, Maintain, and Pit Stop controls
- Save and load race progress
- Audio feedback for actions

---

## How to Play
1. Select your car from the dropdown menu.
2. Watch your car race along the track.
3. Use the buttons to:
   - **Speed Up**: Increase speed but consume more fuel.
   - **Maintain**: Keep current speed and conserve fuel.
   - **Pit Stop**: Refill fuel but skip lap progression.
4. Complete all laps to win the race.
5. Monitor your progress using the **Lap Progress**, **Fuel**, and **Time** bars.

---

## How to Run
1. Clone the repository:
```bash
git clone https://github.com/Maxified01/SpeedRacer.git

Open the solution in Visual Studio.
Build the project.
Run the application (F5 or Debug → Start Debugging).
Ensure the Sounds folder is present with speedup.mp3 and pitstop.mp3.
XML Documentation

All classes and methods include XML documentation. Example:

/// <summary>
/// Moves the car along the track according to lap progression.
/// </summary>
/// <param name="targetX">The target X position on the canvas</param>
private void AnimateCar(double targetX)
{
    ...
}
Project Structure
SpeedRacer/
│
├─ MainWindow.xaml
├─ MainWindow.xaml.cs
├─ Models/
│   └─ Car.cs
├─ Managers/
│   └─ RaceManager.cs
├─ Enums/
│   └─ GameAction.cs
├─ Sounds/
│   ├─ speedup.mp3
│   └─ pitstop.mp3
├─ UnitTests/
│   └─ RaceManagerTests.cs
└─ README.md


classDiagram
    MainWindow --> RaceManager
    RaceManager --> Car
    RaceManager --> GameAction

    class MainWindow {
        +AnimateCar()
        +UpdateUI()
    }

    class RaceManager {
        -CurrentLap
        -TotalLaps
        -TimeRemaining
        +TakeTurn()
        +IsRaceFinished()
    }

    class Car {
        -Name
        -Speed
        -MaxFuel
        -CurrentFuel
        +ConsumeFuel()
        +Refuel()
    }

    class GameAction {
        <<enum>>
        SpeedUp
        Maintain
        PitStop
    }
