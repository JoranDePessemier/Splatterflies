using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private Music[] _musicInGame;

    private void Awake()
    {
        if (MusicManager.Instance != null)
        {        
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        foreach (var music in _musicInGame)
        {
            CreateSource(music);
            music.source.Play();
            music.source.volume = 0;
        }
    }

    private void CreateSource(Music music)
    {
        music.source = this.AddComponent<AudioSource>();
        music.source.volume = music.Volume;
        music.source.clip = music.Clip;
        music.source.playOnAwake = false;
        music.source.loop = music.Looping;
    }

    public void StartFadeIn(string name,float fadeSpeed)
    {
        Music neededMusic = FindMusicByName(name);

        StartCoroutine(FadeIn(neededMusic, fadeSpeed));
    }

    public void StartFadeOut(string name, float fadeSpeed)
    {
        Music neededMusic = FindMusicByName(name);

        StartCoroutine(FadeOut(neededMusic, fadeSpeed));
    }

    private Music FindMusicByName(string name)
    {
        Music neededMusic = new Music();

        foreach (Music music in _musicInGame)
        {
            if (music.Name == name)
            {
                neededMusic = music;
            }
        }

        return neededMusic;
    }

    private  IEnumerator FadeOut(Music music, float fadeSpeed)
    {
        while(music.source.volume > 0)
        {
            music.source.volume = Mathf.MoveTowards(music.source.volume,0, fadeSpeed * Time.unscaledDeltaTime);
            yield return 0;
        }
    }

    private  IEnumerator FadeIn(Music music, float fadeSpeed)
    {
        while (music.source.volume < music.Volume)
        {
            music.source.volume = Mathf.MoveTowards(music.source.volume, music.Volume, fadeSpeed * Time.unscaledDeltaTime);
            yield return 0;
        }
    }

    internal void StopAllMusic()
    {
        foreach(Music music in _musicInGame)
        {
            if (music.source.isPlaying)
            {
                music.source.volume = 0;
            }
    
        }
    }
}
