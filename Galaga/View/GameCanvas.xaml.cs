﻿using System;
using System.Diagnostics;
using Galaga.Model;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Galaga.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameCanvas
    {
        private readonly GameManager gameManager;
        private readonly DispatcherTimer gameDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCanvas"/> class.
        /// </summary>
        public GameCanvas()
        {
            this.InitializeComponent();

            Width = this.canvas.Width;
            Height = this.canvas.Height;

            ApplicationView.PreferredLaunchViewSize = new Size { Width = Width, Height = Height };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(Width, Height));
            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;

            this.gameManager = new GameManager(this.canvas);

            this.gameDispatcher = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 200) };

            this.gameDispatcher.Tick += this.timerTickMoveLeft;
            this.gameDispatcher.Tick += this.timerTickMoveBullet;
            this.gameDispatcher.Start();
        }   

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Space:
                    this.gameManager.ShootPlayerBullet();
                    break;
            }
        }

        private void timerTickMoveBullet(object sender, object e)
        {
            this.gameManager.MovePlayerBullet();
            this.gameManager.RemoveStruckEnemyAndBullet();
        }
        private void timerTickMoveLeft(Object sender, object e)
        {
            if (this.gameManager.EnemiesDoneMovingInDirection())
            {
                //Remove the move enemies left tick event
                this.gameDispatcher.Tick -= this.timerTickMoveLeft;
                //Adds the move enemies right tick event
                this.gameDispatcher.Tick += this.timerTickMoveRight;
                this.gameManager.ResetEnemyStepsTakenInEachDirection();
                //After first change of direction you have to double the steps taken to position the sprite past its starting position in both directions
                this.gameManager.IncreaseStepsTaken();
            }
            else
            {
                this.gameManager.MoveEnemiesLeft();
            }
            
        }

        private void timerTickMoveRight(Object sender, object e)
        {
            if (this.gameManager.EnemiesDoneMovingInDirection())
            {
                //Remove the move enemies right tick event
                this.gameDispatcher.Tick -= this.timerTickMoveRight;
                //Adds the move enemies left tick event
                this.gameDispatcher.Tick += this.timerTickMoveLeft;

                this.gameManager.ResetEnemyStepsTakenInEachDirection();
            }
            else
            {
                this.gameManager.MoveEnemiesRight();
            }
            
        }
    }
}
