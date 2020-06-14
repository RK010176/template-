using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class AudioManager
    {
        private AudioSource _audioSource;
        public AudioSource AudioSource
        {
            get { return _audioSource; }
            set { _audioSource = value; }
        }
        private List<AudioClip> _sounds;
        public List<AudioClip> Sounds
        {
            get {return _sounds ; }
            set {_sounds = value ; }
        }

        public void PlaySound(int num, bool isLoop)
        {
            _audioSource.clip = _sounds[num];
            _audioSource.loop = isLoop;
            _audioSource.Play();
        }
    }
}