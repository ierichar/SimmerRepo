using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Simmer.VN
{
    public class VN_AudioManager : MonoBehaviour
    {
        private VN_Manager manager;

        public AudioClip buttonClick;

        public List<AudioClip> audioList = new List<AudioClip>();
        public List<AudioSource> standAloneSources = new List<AudioSource>();
        public AudioMixerGroup masterMixer;

        private Dictionary<string, AudioSource> audioDict = new Dictionary<string, AudioSource>();

        public void Construct(VN_Manager manager)
        {
            this.manager = manager;

            //foreach(AudioClip sound in audioList)
            //{
            //    AudioSource newSource = gameObject.AddComponent<AudioSource>();
            //    newSource.playOnAwake = false;
            //    newSource.outputAudioMixerGroup = masterMixer;
            //    newSource.clip = sound;

            //    audioDict.Add(sound.name.ToString(), newSource);
            //}

            //foreach(AudioSource source in standAloneSources)
            //{
            //    source.outputAudioMixerGroup = masterMixer;
            //}
        }

        public bool PlayAudio(string audioName)
        {
            if (audioDict.ContainsKey(audioName))
            {
                audioDict[audioName].Play();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlayAudio(AudioClip clip)
        {
            //string audioName = clip.name.ToString();

            //if (audioDict.ContainsKey(audioName))
            //{
            //    audioDict[audioName].Play();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }
    }
}