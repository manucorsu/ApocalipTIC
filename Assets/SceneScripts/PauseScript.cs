using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private GameObject pauseMenu; //panel PauseMenu
    private GameObject[] tiles;


    void Awake()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }
    }

    public void SetPause(bool s)
    // pausa/despausa el juego, abriendo/cerrando la 
    {
        isPaused = s;
        if (s == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            bool dv = GameObject.Find("Botones").GetComponent<scrBotones>().dv;

            if (dv == true) Time.timeScale = 2;
            else { Time.timeScale = 1; }

            pauseMenu.SetActive(false);
        }
    }
}
