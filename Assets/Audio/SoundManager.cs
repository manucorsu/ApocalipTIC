using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private bool sfxOn = true;
    private bool musOn = true;
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip uiClick;
    [SerializeField] private Sprite btnOnSpr;
    [SerializeField] private Sprite btnOffSpr;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else instance = this;

        DontDestroyOnLoad(this);
    }

    public void PlayUIClick() => PlaySound(uiClick, 3);

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        if (sfxOn && clip != null)
        {
            sfxPlayer.volume = volume;
            sfxPlayer.PlayOneShot(clip);
        }
    }

    public void ToggleSFX(Image callerButtonImg)
    {
        sfxOn = !sfxOn;
        if (callerButtonImg.sprite == btnOnSpr)
        {
            callerButtonImg.sprite = btnOffSpr;
        }
        else if (callerButtonImg.sprite == btnOffSpr)
        {
            callerButtonImg.sprite = btnOnSpr;
        }
        else
        {
            throw new System.Exception($"{callerButtonImg.sprite.name} is an invalid sprite for SFX toggler.");
        }
    }

    public void ToggleMus(Image callerButtonImg)
    {
        musOn = !musOn;
        if(callerButtonImg.sprite == btnOnSpr)
        {
            callerButtonImg.sprite = btnOffSpr;
        }
        else if(callerButtonImg.sprite == btnOffSpr)
        {
            callerButtonImg.sprite = btnOnSpr;
        }
        else
        {
            throw new System.Exception($"{callerButtonImg.sprite.name} is an invalid sprite for mus toggler.");
        }
    }
}
