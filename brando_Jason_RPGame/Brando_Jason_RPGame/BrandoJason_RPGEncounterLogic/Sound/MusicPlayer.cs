using System;
using System.Collections.Generic;
using System.Text;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.CoreAudioAPI;

namespace BrandoJason_RPGEncounterLogic.Sound
{
    public class MusicPlayer : IDisposable
    {
        private ISoundOut _soundOut;
        private IWaveSource _waveSource;

        public event EventHandler<PlaybackStoppedEventArgs> PlaybackStopped;

        public PlaybackState PlaybackState
        {
            get
            {
                if (_soundOut != null)
                    return _soundOut.PlaybackState;
                return PlaybackState.Stopped;
            }
        }

        private Playable Open(Track track)
        {
            OpenWithoutPlayable(track);
            return new Playable(this, track);
        }

        public void OpenWithoutPlayable(Track track)
        {
            this.OpenInternal(track.GetFile(), GetMMDevices());
        }

        private void OpenInternal(string filename, MMDevice device)
        {
            CleanupPlayback();

            _waveSource =
                CodecFactory.Instance.GetCodec(filename)
                    .ToSampleSource()
                    .ToStereo()
                    .ToWaveSource().Loop();
            _soundOut = new WasapiOut() { Latency = 100, Device = device };
            _soundOut.Initialize(_waveSource);
            if (PlaybackStopped != null) _soundOut.Stopped += PlaybackStopped;
        }

        public static MMDevice GetMMDevices()
        {
            using (var mmdeviceEnumerator = new MMDeviceEnumerator())
            {
                using (var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
                {
                    foreach (var device in mmdeviceCollection)
                    {
                        return device;
                    }
                }
            }
            return null;
        }

        public void Play()
        {
            if (_soundOut != null)
                _soundOut.Play();
        }

        public void Pause()
        {
            if (_soundOut != null)
                _soundOut.Pause();
        }

        public void Stop()
        {
            if (_soundOut != null)
                _soundOut.Stop();
        }

        private void CleanupPlayback()
        {
            if (_soundOut != null)
            {
                _soundOut.Dispose();
                _soundOut = null;
            }
            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }
        }

        public void Dispose()
        {
            CleanupPlayback();
        }
    }

}
