using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CSCore.SoundOut;
using BrandoJason_RPGEncounterLogic.Sound;

namespace BrandoJason_RPGEncounterLogic.Sound
{
    public class Playable : IDisposable
    {
        public MusicPlayer _player { get; private set; }
        public Track Track { get; private set; }
        private bool StopRequested { get; set; }

        public Playable(MusicPlayer player)
        {
            _player = player;
            this.Track = 0;
        }

        public void Play(bool open = true)
        {
            StopRequested = false;

            if(open)
            {
                _player.OpenWithoutPlayable(this.Track);
            }

            _player.Play();
        }

        public void Stop()
        {
            StopRequested = true;
            this.Dispose();
            _player.Stop();
        }

       
        public void Dispose()
        {
            _player?.Stop();
            _player?.Dispose();
        }
    }

    public enum Track
    {
         Battle = 0
    }

    public static class EnumExtensions
    {
        public static string GetFile(this Track track)
        {
            var fileName = "";
            switch (track)
            {
                case Track.Battle:
                    fileName = "Lufia_2_Music_-_Battle_Theme1-NFvwTKzWrLg.wav";
                    break;
                default:
                    break;
            }

            return fileName;
        }
    }
}