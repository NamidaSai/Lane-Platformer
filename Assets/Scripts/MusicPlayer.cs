﻿using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] Sound[] tracks = null;
    [SerializeField] string startingTrack = null;

    public static MusicPlayer instance;

    private Sound currentTrack = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound track in tracks)
        {
            track.source = gameObject.AddComponent<AudioSource>();
            track.source.clip = track.clip;
            track.source.pitch = track.pitch;
            track.source.loop = track.loop;
            track.source.volume = track.volume;
        }
    }

    private void Start()
    {
        Play(startingTrack);
    }

    public void Play(string trackName)
    {
        Sound track = Array.Find(tracks, trackClip => trackClip.name == trackName);

        if (track == null)
        {
            Debug.LogWarning("Sound: " + trackName + " not found.");
            return;
        }

        if (track.source == null)
        {
            return;
        }

        if (currentTrack != null && currentTrack.source.isPlaying)
        {
            StartCoroutine(SwitchTrack(track));
            return;
        }

        track.source.Play();
        currentTrack = track;
    }

    private IEnumerator SwitchTrack(Sound nextTrack)
    {
        currentTrack.source.loop = false;
        yield return new WaitWhile(() => currentTrack.source.isPlaying);
        nextTrack.source.Play();
        Destroy(currentTrack.source);
        currentTrack = nextTrack;
    }
}
