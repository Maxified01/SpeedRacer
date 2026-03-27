using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using SpeedRacer.Models;
using SpeedRacer.Managers;
using SpeedRacer.Enums;

namespace SpeedRacer
{
    public partial class MainWindow : Window
    {
        private RaceManager raceManager;
        private readonly DispatcherTimer timer;
        private readonly string saveFile = "race_save.txt";
        private readonly MediaPlayer speedUpPlayer;
        private readonly MediaPlayer pitStopPlayer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Timer_Tick;

            speedUpPlayer = new MediaPlayer();
            pitStopPlayer = new MediaPlayer();

            // Add cars
            CarSelector.Items.Add(new Car("Roadstar", 10, 5, 100));
            CarSelector.Items.Add(new Car("Turbocharger", 12, 8, 80));
            CarSelector.Items.Add(new Car("Challenger", 7, 3, 120));
            CarSelector.SelectedIndex = 0;

            LoadRace();

            if (raceManager == null)
                StartRace();
        }

        private void StartRace()
        {
            raceManager = new RaceManager((Car)CarSelector.SelectedItem);
            timer.Start();
            UpdateUI();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!raceManager.IsRaceOver)
            {
                raceManager.TakeTurn(GameAction.Maintain);
                AnimateCar(GetCarPosition());
                UpdateUI();
            }
            else
            {
                timer.Stop();
                SaveRace();
            }
        }

        private void UpdateUI()
        {
            CarStats.Text = $"Speed: {raceManager.PlayerCar.Speed} | Fuel: {raceManager.PlayerCar.CurrentFuel}";
            LapLabel.Content = $"Lap: {raceManager.CurrentLap}/{raceManager.TotalLaps}";
            LapProgressBar.Value = (double)raceManager.CurrentLap / raceManager.TotalLaps * 100;

            FuelBar.Value = raceManager.PlayerCar.CurrentFuel;
            TimeBar.Value = raceManager.TimeRemaining;

            StatusText.Text = raceManager.IsRaceOver
                ? (raceManager.PlayerWon ? "You Win!" : "You Lost!")
                : "Racing...";
        }

        private double GetCarPosition()
        {
            double trackWidth = TrackCanvas.ActualWidth - CarRectangle.Width;
            double progress = (double)raceManager.CurrentLap / raceManager.TotalLaps;
            return trackWidth * progress;
        }

        private void AnimateCar(double targetX)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = targetX,
                Duration = TimeSpan.FromSeconds(0.8),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            CarRectangle.BeginAnimation(Canvas.LeftProperty, animation);
        }

        private void SpeedUp_Click(object sender, RoutedEventArgs e)
        {
            PlaySound(speedUpPlayer, "Sounds/speedup.mp3");
            raceManager.TakeTurn(GameAction.SpeedUp);
            AnimateCar(GetCarPosition());
            UpdateUI();
        }

        private void Maintain_Click(object sender, RoutedEventArgs e)
        {
            raceManager.TakeTurn(GameAction.Maintain);
            AnimateCar(GetCarPosition());
            UpdateUI();
        }

        private void PitStop_Click(object sender, RoutedEventArgs e)
        {
            PlaySound(pitStopPlayer, "Sounds/pitstop.mp3");
            raceManager.TakeTurn(GameAction.PitStop);
            AnimateCar(GetCarPosition());
            UpdateUI();
        }

        private void CarSelector_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (timer != null)
                StartRace();
        }

        private void SaveRace()
        {
            try
            {
                string data = $"{raceManager.CurrentLap},{raceManager.PlayerCar.CurrentFuel},{raceManager.TimeRemaining}";
                File.WriteAllText(saveFile, data);
            }
            catch { }
        }

        private void LoadRace()
        {
            try
            {
                if (!File.Exists(saveFile)) return;

                string[] data = File.ReadAllText(saveFile).Split(',');

                raceManager = new RaceManager((Car)CarSelector.SelectedItem);

                raceManager.SetCurrentLap(int.Parse(data[0]));
                raceManager.SetPlayerFuel(int.Parse(data[1]));
                raceManager.SetTimeRemaining(int.Parse(data[2]));
            }
            catch
            {
                raceManager = null;
            }
        }

        private void PlaySound(MediaPlayer player, string path)
        {
            if (!File.Exists(path)) return;

            player.Open(new Uri(System.IO.Path.GetFullPath(path)));
            player.Stop();
            player.Play();
        }
    }
}