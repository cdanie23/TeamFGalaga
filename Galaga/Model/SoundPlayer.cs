using System;
using System.IO;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace Galaga.Model
{
    /// <summary>
    ///     The Sound Player
    /// </summary>
    public static class SoundPlayer
    {
        #region Data members

        private const string ShootPath = "Audio/shot.wav";
        private const string ExplosionPath = "Audio/explosion.wav";
        private const string WinPath = "Audio/win.wav";
        private const string LosePath = "Audio/lose.wav";
        private const string BonusActivePath = "Audio/bonusactive.wav";
        private const string BonusGottenPath = "Audio/bonusgotten.wav";
        private const string PowerUpPath = "Audio/powerup.mp3";
        private const double VolumeDecrease = 0.05;

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the shoot sound.
        /// </summary>
        public static void playShootSound()
        {
            var mediaPlayer = setUpMediaPlayer(ShootPath);
            decreaseVolume(mediaPlayer);
            mediaPlayer.Play();
        }

        private static void decreaseVolume(MediaPlayer mediaPlayer)
        {
            mediaPlayer.Volume = VolumeDecrease;
        }

        /// <summary>
        ///     Plays the exploding sound.
        /// </summary>
        public static void playExplodeSound()
        {
            var mediaPlayer = setUpMediaPlayer(ExplosionPath);
            decreaseVolume(mediaPlayer);
            mediaPlayer.Play();
        }

        /// <summary>
        ///     Plays the win sound.
        /// </summary>
        public static void playWinSound()
        {
            setUpMediaPlayer(WinPath).Play();
        }

        /// <summary>
        ///     Plays the lose sound.
        /// </summary>
        public static void playLoseSound()
        {
            setUpMediaPlayer(LosePath).Play();
        }

        /// <summary>
        ///     Plays the bonus active sound.
        /// </summary>
        public static void playBonusActiveSound()
        {
            setUpMediaPlayer(BonusActivePath).Play();
        }

        /// <summary>
        ///     Plays the bonus gotten sound.
        /// </summary>
        public static void playBonusGottenSound()
        {
            setUpMediaPlayer(BonusGottenPath).Play();
        }

        /// <summary>
        ///     Plays the power up sound.
        /// </summary>
        public static void playPowerUpSound()
        {
            setUpMediaPlayer(PowerUpPath).Play();
        }

        /// <summary>
        ///     Sets up media player.
        /// </summary>
        /// <param name="audioPath">The audio path.</param>
        /// <returns> the media player </returns>
        private static MediaPlayer setUpMediaPlayer(string audioPath)
        {
            var mediaPlayer = new MediaPlayer();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, audioPath);
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(fullPath));
            return mediaPlayer;
        }

        #endregion
    }
}