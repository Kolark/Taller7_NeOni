﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

/// <summary>
/// It manages the sound and saves them in a dictionary
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Since the object containing this script will be a prefab and in DontDestroyOnLoad(),
    /// the staticSounds Property refers to those sound that will be present most of the time.
    /// </summary>
    public Sound[] staticSounds;

    Sound[] externalSounds;

    Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

    private static AudioManager instance;
    public static AudioManager Instance => instance;

    void Awake()
    {
        #region singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        #region setSounds
        for (int i = 0; i < staticSounds.Length; i++)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            staticSounds[i].SetAudioSource(ref src);
        }
        #endregion
    }
    
    void AddToDictionary(Sound[] sounds)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            soundDictionary.Add(sounds[i].name, sounds[i]);
        }
    }

    void RemoveFromDictionary(Sound[] sounds)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            soundDictionary.Remove(sounds[i].name);
        }
    }

    /// <summary>
    /// Searchs the given name in the dictionary and plays it if found
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = soundDictionary[name];
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    /// <summary>
    /// Checks if the given sound name is Playing
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsPlaying(string name)
    {
        return soundDictionary[name].source.isPlaying;
    }
    /// <summary>
    /// Stops Playing the if the sound of the name given exists
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)
    {
        Sound s = soundDictionary[name];
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    /// <summary>
    /// Is able to receive
    /// </summary>
    /// <param name="package"></param>
    public void ReceiveExternal(Sound[] sounds)
    {
        RemoveFromDictionary(externalSounds);
        externalSounds = sounds;
        AddToDictionary(sounds);
    }
}