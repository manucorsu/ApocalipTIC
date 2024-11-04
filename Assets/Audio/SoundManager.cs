﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private bool sfxOn = true;
    private bool musOn = true;
    [SerializeField] public AudioSource sfxPlayer;
    [SerializeField] private AudioClip uiClick;
    [field: SerializeField] public AudioClip BuySfx { get; private set; }
    public Sprite onSpr;
    public Sprite offSpr;
    public AudioSource audioSource;
    public AudioClip temaPrincipal;
    private List<AudioSource> loopers = new List<AudioSource>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;

        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayUIClick() => PlaySound(uiClick);

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        if (volume < 0 || volume > 1)
        {
            Application.Quit();
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }
        if (clip == null)
        {
            throw new System.ArgumentNullException("clip", "el clip pasado fue null");
        }
        if (sfxOn)
        {
            sfxPlayer.volume = volume;
            sfxPlayer.PlayOneShot(clip);
        }
    }
    
    public void PlaySounds(AudioClip[] clips, float[] volumes, float waitTime)
    {
        if (clips.Length != volumes.Length)
        {
            throw new System.ArgumentException(message: "clips y volumes deberían tener el mismo length");
        }
        foreach (float v in volumes)
        {
            if (v < 0 || v > 1)
            {
                throw new System.ArgumentOutOfRangeException(paramName: "volumes", message: "Todos los volúmenes tienen que ser un float entre 0 y 1 porque Unity.");
            }
        }
        if (waitTime < 0)
        {
            throw new System.ArgumentOutOfRangeException(paramName: "waitTime", message: "waitTime no puede ser negativo.");
        }

        StartCoroutine(PlaySoundsCoroutine(clips, volumes, waitTime));
    }

    public void LoopSound(AudioSource player, AudioClip clip, float volume)
    {
        if (player == null) throw new System.ArgumentNullException("player");
        if (clip == null) throw new System.ArgumentNullException("clip");
        if (volume < 0 || volume > 1)
        {
            Application.Quit();
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }

        if (loopers.Contains(player) == false)
        {
            player.volume = volume;
            player.clip = clip;
            player.loop = true;
            player.Play();
            loopers.Add(player);
        }
        else
        {
            Debug.LogWarning("Ese AudioSource ya estaba loopeando. No se hizo nada.");
        }
    }

    public void StopSFXLoop(AudioSource player)
    {
        if (player == null) throw new System.ArgumentNullException("player");
        if (loopers.Contains(player) == false)
        {
            throw new System.Exception("El player pasado no estaba loopeando");
        }

        player.loop = false;
        player.Stop();
        loopers.Remove(player);
    }

    public void StopAllSfxLoops()
    {
        List<AudioSource> lIterable = new List<AudioSource>(loopers);
        foreach(AudioSource l in lIterable)
        {
            l.loop = false;
            l.Stop();
            loopers.Remove(l);
        }
    }

    public void SetPauseAllSfxLoops(bool pause)
    {
        List<AudioSource> lIterable = new List<AudioSource>(loopers);
        foreach (AudioSource l in lIterable)
        {
            l.Pause();
        }
    }

    public void PlayMus(AudioClip clip, float volume = 1)
    {

        if (volume < 0 || volume > 1)
        {
            Application.Quit();
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }
        if (musOn && clip != null)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip);
        }
    }

    public void ToggleSFX(Image callerButtonImg)
    {
        sfxOn = !sfxOn;
        //if(sfxOn == false)
        //{
        //    List<AudioSource> loopersClone = loopers.ToList();
        //    foreach (AudioSource looper in loopersClone) looper?.Stop();
        //}
        if (callerButtonImg.sprite == onSpr)
        {
            callerButtonImg.sprite = offSpr;
        }
        else if (callerButtonImg.sprite == offSpr)
        {
            callerButtonImg.sprite = onSpr;
        }
        else
        {
            throw new System.Exception($"{callerButtonImg.sprite.name} is an invalid sprite for SFX toggler.");
        }
    }

    public void ToggleMus(Image callerButtonImg)
    {
        musOn = !musOn;

        if (musOn)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 0;
        }

        if (callerButtonImg.sprite == onSpr)
        {
            callerButtonImg.sprite = offSpr;
        }
        else if (callerButtonImg.sprite == offSpr)
        {
            callerButtonImg.sprite = onSpr;
        }
        else
        {
            throw new System.Exception($"{callerButtonImg.sprite.name} is an invalid sprite for mus toggler.");
        }
    }

    public Dictionary<string, bool> GetStates()
    {
        return new Dictionary<string, bool>
        {
            { "sfxOn", sfxOn },
            { "musOn", musOn }
        };
    }

    private IEnumerator PlaySoundsCoroutine(AudioClip[] clips, float[] volumes, float waitTime)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            PlaySound(clips[i], volumes[i]);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
