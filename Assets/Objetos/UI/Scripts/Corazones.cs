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
#if !UNITY_EDITOR
        if(EnemySpawner.vidas == 0)
            SceneManager.LoadScene("GameOver");
#endif
        for(byte i = 0; i < corazones.Length; i++)
        {
            if(statusCorazones[i] == false)
            {
                corazones[i].GetComponent<Animator>().SetTrigger("romper");
                statusCorazones[i] = true;
                break;
            }
        }
    }
}