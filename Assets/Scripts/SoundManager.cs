using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f,1f)]
        public float volume = 1;
        [Range(0.5f, 1.5f)]
        public float pitch = 1;

        private AudioSource source;

        public void SetSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
        }
        public void Play()
        {
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }

    [SerializeField]
    Sound[] sounds;

    public static SoundManager soundManager;

    void Awake()
    {
        soundManager = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }    
    }
    void Update()
    {
    }
    public void PlayAudio(string _audio)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _audio)
            {
                sounds[i].Play();
                return;
            }
        }
    }
}
