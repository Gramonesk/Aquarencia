using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Play, PlayOneShot
    }
    [Serializable]
    public struct Sound
    {
        public AudioClip clip;
        public string name;
    }
    [Serializable]
    public struct Audio
    {
        public AudioSource source;
        public SoundType type;
        public List<Sound> sounds;
    }
    public List<Audio> AudioList;

    public string StartAudio;
    public static AudioManager Instance;
    public void Play(string name)
    {
        foreach(Audio AUD in AudioList)
        {
            int toPlay = AUD.sounds.FindIndex(x => x.name == name);
            if (toPlay == -1) continue;
            switch (AUD.type)
            {
                case SoundType.Play:
                    AUD.source.clip = AUD.sounds[toPlay].clip;
                    AUD.source.Play();
                    break;
                case SoundType.PlayOneShot:
                    AUD.source.PlayOneShot(AUD.sounds[toPlay].clip);
                    break;
                default:
                    break;
            }
            return;
        }
        Debug.Log("not found");
    }
    private void Awake()
    {
        if(Instance != null)
        {
            if(this.StartAudio != "none")
            Instance.Play(this.StartAudio);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Play(StartAudio);
    }
}
