using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Corazones : MonoBehaviour
{
    public static Corazones Instance { get; private set; }
    [SerializeField] private GameObject[] corazones;

    [SerializeField] private AudioClip upgradeSfx;

    [SerializeField] private AudioSource audioSource;
    private bool[] statusCorazones = new bool[] { false, false, false };

    [SerializeField] private LevelChanger levelChanger;
    //cada corazón está en `false` si NO está roto y en `true` si está roto


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GiveLife();
        }
    }

    public void LoseLife(GameObject losingEnemyGO)
    {
        EnemySpawner.vidas--;
        if (MessageBox.Instance.CheatsEnabled == false && EnemySpawner.vidas == 0)
        {
            SoundManager.Instance.StopAllSfxLoops();
            SoundManager.Instance.GetComponent<AudioSource>().clip = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().musica[3];
            SoundManager.Instance.GetComponent<AudioSource>().Play();
            SoundManager.Instance.GetComponent<AudioSource>().loop = false;
            GameOverManager.LoseGame(losingEnemyGO, levelChanger);
        }
        for (int i = 0; i < corazones.Length; i++)
        {
            if (statusCorazones[i] == false)
            {
                corazones[i].GetComponent<Animator>().SetTrigger("romper");
                statusCorazones[i] = true;
                break;
            }
        }
    }

    public void GiveLife()
    {
        if ((EnemySpawner.vidas + 1) <= 3)
        {
            EnemySpawner.vidas++;
            SoundManager.Instance.PlaySound(audioSource, upgradeSfx);
            for (int i = corazones.Length - 1; i >= 0; i--)
            {
                if (corazones[i] != null && statusCorazones[i] == true)
                {
                    corazones[i].GetComponent<Animator>().SetTrigger("unromper");
                    statusCorazones[i] = false;
                    break;
                }
            }
        }
    }
}