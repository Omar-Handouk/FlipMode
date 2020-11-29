using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else if (Instance != this) {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = this.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = (sound.is2D ? 0f : 1f);
        }
    }
    
    public void Play(string name) {
        if (!GameManager.muteAll) {
            Sound sound = Array.Find(sounds, s => s.name == name);

            if (sound == null) {
                Debug.LogWarning("Un-able to play sound!\nSound: " + name + " was not found!");
                return;
            }

            if (sound.isOneShot) {
                sound.source.PlayOneShot(sound.source.clip, 1f);
            } else {
                sound.source.Play();
            }
        }
    }

    public void Stop(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null) {
            Debug.LogWarning("Un-able to stop sound!\nSound: " + name + " was not found!");
            return;
        }

        sound.source.Stop();
    }

    public void StopAll() {
        string[] themes = {"Calm", "Upbeat"};
        
        foreach (string theme in themes)
        {
            this.Stop(theme);
        }
    }

    public void Pause(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null) {
            Debug.LogWarning("Un-able to pause sound!\nSound: " + name + " was not found!");
            return;
        }

        sound.source.Pause();
    }

    public void UnPause(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null) {
            Debug.LogWarning("Un-able to un-pause sound!\nSound: " + name + " was not found!");
            return;
        }

        sound.source.UnPause();
    }
}
