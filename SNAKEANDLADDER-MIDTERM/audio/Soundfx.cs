using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NAudio.Wave;
using System.Media;

namespace TICTACTOE_MIDTERM.audio
{
    internal class Soundfx
    {

        string baseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

        public void PlayErrorSound()
        {

            string relativePath = Path.Combine(baseDirectory, "audio", "sounds","bruh.mp3");

            Task.Run(() =>
            {
                if (!File.Exists(relativePath))
                {
                    return;
                }

                using (var errSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    errSound.Init(reader);
                    errSound.Play();

                    while (errSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }

            });
            
            
            
        }

        public void PlayWinSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds","winning.wav");
            Task.Run(() =>
            {
                using (var winSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    winSound.Init(reader);
                    winSound.Play();

                    while (winSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }

                }
            });
        }

        public void PlayObtainSkill()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds" ,"win.wav");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void climbLadderSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "ladder.wav");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void snakeBiteSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "snake.mp3");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void stunSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "stun.wav");
            Task.Run(() =>
            {
                using (var stunSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    stunSound.Init(reader);
                    stunSound.Play();

                    while (stunSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }

                }
            });
        }

        public void sabotageSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "sabotage.wav");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void swapSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "swap.wav");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void PlayMenuSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds","mixkit-unlock-game-notification-253.wav");
            Task.Run(() =>
            {
                using (var menuSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    menuSound.Init(reader);
                    menuSound.Play();

                    while (menuSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void shieldSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "shield.mp3");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }

        public void anchorSound()
        {
            string relativePath = Path.Combine(baseDirectory, "audio", "sounds", "anchor.mp3");
            Task.Run(() =>
            {
                using (var placeSound = new WaveOutEvent())
                using (var reader = new AudioFileReader(relativePath))
                {
                    placeSound.Init(reader);
                    placeSound.Play();

                    while (placeSound.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            });
        }


    }
}
