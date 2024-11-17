using System.Collections;
using System.Collections.Generic;
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
    public AudioSource musPlayer;
    public AudioClip temaPrincipal;
    private List<AudioSource> loopers = new List<AudioSource>();

    [SerializeField] private int warns = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;

        DontDestroyOnLoad(this);

        musPlayer = GetComponent<AudioSource>();
    }

    public void PlayUIClick() => PlaySound(sfxPlayer, uiClick);
    public void PlayBuySfx() => PlaySound(sfxPlayer, BuySfx, 0.4f);

    public void PlaySound(AudioSource source, AudioClip clip, float volume = 1)
    {
        if (volume < 0 || volume > 1)
        {
            throw new System.ArgumentOutOfRangeException("volume", "volume debe ser un float entre 0 y 1 porque Unity.");
        }
        if (source == null)
        {
            throw new System.ArgumentNullException("source");
        }
        if (clip == null)
        {
            throw new System.ArgumentNullException("clip");
        }

        if (sfxOn)
        {
            if (source.isActiveAndEnabled)
            {
                source.volume = volume;
                source.PlayOneShot(clip);
            }
            else
            {
                //fallback por si justo se destruye el objeto
                //no es para nada ideal pero bueno, por lo menos te reta en el inspector con warns
                warns++;
                PlaySound(sfxPlayer, clip, volume);
            }
        }
    }

    public void PlaySounds(AudioSource source, AudioClip[] clips, float[] volumes, float waitTime)
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

        StartCoroutine(PlaySoundsCoroutine(source, clips, volumes, waitTime));
    }

    public void LoopSound(AudioSource player, AudioClip clip, float volume)
    {
        if (sfxOn)
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
    }

    public void StopSFXLoop(AudioSource player)
    {
        if (player == null) throw new System.ArgumentNullException("player");
        if (loopers.Contains(player) == false)
        {
            Debug.LogWarning("El player pasado no estaba loopeando. No se hizo nada.");
            return;
        }

        player.loop = false;
        player.Stop();
        loopers.Remove(player);
    }

    public void StopAllSfxLoops()
    {
        List<AudioSource> lIterable = new List<AudioSource>(loopers);
        foreach (AudioSource l in lIterable)
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
            musPlayer.volume = volume;
            musPlayer.PlayOneShot(clip);
        }
    }

    public void ToggleSFX(Image callerButtonImg)
    {
        sfxOn = !sfxOn;
        if (sfxOn == false) StopAllSfxLoops();
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
            musPlayer.volume = 1;
        }
        else
        {
            musPlayer.volume = 0;
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

    private IEnumerator PlaySoundsCoroutine(AudioSource source, AudioClip[] clips, float[] volumes, float waitTime)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (sfxOn) PlaySound(source, clips[i], volumes[i]);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
