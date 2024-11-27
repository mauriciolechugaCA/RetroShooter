using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

/*
 * Manages all sound effects in the game.
 */

namespace RetroShooter.Managers
{
    public class SoundManager
    {
        private Dictionary<string, SoundEffect> _soundEffects;
        private Dictionary<string, Song> _songs;
        private float _soundEffectVolume;
        private float _musicVolume;

        public SoundManager()
        {
            _soundEffects = new Dictionary<string, SoundEffect>();
            _songs = new Dictionary<string, Song>();
            _soundEffectVolume = 1.0f; // Default volume
            _musicVolume = 0.2f; // Default volume
        }

        public void LoadSoundEffect(string name, SoundEffect soundEffect)
        {
            _soundEffects[name] = soundEffect;
        }

        public void LoadSong(string name, Song song)
        {
            _songs[name] = song;
        }

        public void PlaySoundEffect(string name)
        {
            if (_soundEffects.TryGetValue(name, out var soundEffect))
            {
                soundEffect.Play(_soundEffectVolume, 0.0f, 0.0f);
            }
        }

        public void PlaySong(string name, bool isRepeating = true)
        {
            if (_songs.TryGetValue(name, out var song))
            {
                MediaPlayer.Volume = _musicVolume;
                MediaPlayer.IsRepeating = isRepeating;
                MediaPlayer.Play(song);
            }
        }

        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        public void SetSoundEffectVolume(float volume)
        {
            _soundEffectVolume = volume;
        }

        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            MediaPlayer.Volume = volume;
        }
    }
}