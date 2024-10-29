using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private bool sfxOn = true;
    private bool musOn = true;
    [SerializeField] public AudioSource sfxPlayer;
    [SerializeField] private AudioClip uiClick;
    public Sprite onSpr;
    public Sprite offSpr;
    public AudioSource audioSource;
    public AudioClip temaPrincipal;
    private HashSet<AudioSource> loopers;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else instance = this;

        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayUIClick() => PlaySound(uiClick);

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        if(volume < 0 || volume > 1)
        {
            Application.Quit();
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }
        if (sfxOn && clip != null)
        {
            sfxPlayer.volume = volume;
            sfxPlayer.PlayOneShot(clip);
        }
    }

    public void PlaySound(AudioSource player, AudioClip clip, float volume = 1)
    {
        if (volume < 0 || volume > 1)
        {
            Application.Quit();
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }
        if (sfxOn)
        {
            if (clip != null)
            {
                player.volume = volume;
                player.PlayOneShot(clip);
            }
            else Debug.LogWarning("El clip fue null. No se reprodujo nada.");
        }
    }

    public void LoopSound(AudioSource player, AudioClip clip, float volume = 1)
    {
        if (volume < 0 || volume > 1)
        {
            Application.Quit();
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }
        if (sfxOn)
        {
            if (clip != null)
            {
                loopers.Add(player);
                player.volume = volume;
                player.loop = true;
                player.clip = clip;
            }
            else Debug.LogWarning("El clip fue null. No se loopeó nada.");
        }
    }

    public void StopSoundLoop(AudioSource player)
    {
        loopers.Remove(player);
        player.Stop();
    }

    public void PlayMus(AudioClip clip, float volume = 1) { 
        
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
        if(sfxOn == false)
        {
            List<AudioSource> loopersClone = loopers.ToList();
            foreach (AudioSource looper in loopersClone) looper?.Stop();
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
            throw new System.Exception($"{callerButtonImg.sprite.name} is an invalid sprite for SFX toggler.");
        }
    }

    public void ToggleMus(Image callerButtonImg)
    {
        musOn = !musOn;

        if (musOn)
        {
            audioSource.volume = 1;
        } else
        {
            audioSource.volume = 0;
        }

        if(callerButtonImg.sprite == onSpr)
        {
            callerButtonImg.sprite = offSpr;
        }
        else if(callerButtonImg.sprite == offSpr)
        {
            callerButtonImg.sprite = onSpr;
        }
        else
        {
            throw new System.Exception($"{callerButtonImg.sprite.name} is an invalid sprite for mus toggler.");
        }
    }

    public Dictionary<string, bool> GetStates()
        // devuelve el estado de sfxOn en [0]
        // y el estado de musOn en [1]
    {
        Dictionary<string, bool> output = new Dictionary<string, bool>
        {
            { "sfxOn", sfxOn },
            { "musOn", musOn }
        };
        return output;
    }
}
