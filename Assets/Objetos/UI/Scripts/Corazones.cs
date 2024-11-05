using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Corazones : MonoBehaviour
{
    public static Corazones instance;
    [SerializeField] private GameObject[] corazones;
    private bool[] statusCorazones = new bool[] { false, false, false };
    //cada corazón está en `false` si NO está roto y en `true` si está roto

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else instance = this;
    }

    public void LoseLife()
    {
        EnemySpawner.vidas--;
        if (MessageBox.Instance.CheatsEnabled == false && EnemySpawner.vidas == 0)
        {
            SoundManager.Instance.StopAllSfxLoops();
            SoundManager.Instance.GetComponent<AudioSource>().clip = GameObject.Find("SCENESCRIPTS").GetComponent<EnemySpawner>().musica[3];
            SoundManager.Instance.GetComponent<AudioSource>().Play();
            SoundManager.Instance.GetComponent<AudioSource>().loop = false;
            SceneManager.LoadScene("GameOver");
        }
        for (byte i = 0; i < corazones.Length; i++)
        {
            if (statusCorazones[i] == false)
            {
                corazones[i].GetComponent<Animator>().SetTrigger("romper");
                statusCorazones[i] = true;
                break;
            }
        }
    }
}