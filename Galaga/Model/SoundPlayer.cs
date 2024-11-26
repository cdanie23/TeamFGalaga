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
        /// <summary>
        ///     The media player
        /// </summary>
        private static MediaPlayer mediaPlayer;

        /// <summary>
        ///     Plays the shoot sound.
        /// </summary>
        public static void playShootSound()
        {
            mediaPlayer = new MediaPlayer();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, "Audio/shot.wav");
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(fullPath));
            mediaPlayer.Play();
        }

        /// <summary>
        ///     Plays the exploding sound.
        /// </summary>
        public static void playExplodeSound()
        {
            mediaPlayer = new MediaPlayer();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, "Audio/explosion.wav");
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(fullPath));
            mediaPlayer.Play();
        }

        /// <summary>
        ///     Plays the win sound.
        /// </summary>
        public static void playWinSound()
        {
            mediaPlayer = new MediaPlayer();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, "Audio/win.wav");
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(fullPath));
            mediaPlayer.Play();
        }

        /// <summary>
        /// Plays the lose sound.
        /// </summary>
        public static void playLoseSound()
        {
            mediaPlayer = new MediaPlayer();
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, "Audio/lose.wav");
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(fullPath));
            mediaPlayer.Play();
        }

    }
}
