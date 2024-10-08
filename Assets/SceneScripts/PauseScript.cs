﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenu; //panel PauseMenu
    private GameObject controls; //las cosas que en winforms se llamaban controles (botones, texto) de este panel
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }
    }

    public void SetPause(bool s)
    // pausa/despausa el juego, y activa el panel de pausa
    // como quería que se note que el juego está pausado cuando aparece una messagebox,
    // pero no quería hacer un panel nuevo que sea igual al fondo el panel este, reciclo,
    // usando este panel desactivando los controles (ver definición de controles) si mb == true
    {

        isPaused = s;
        if (s == true)
        {
            pauseMenu.SetActive(true);
            //if (mb == true) controls.SetActive(false);
            //else { controls.SetActive(false); }
            Time.timeScale = 0;
        }
        else
        {
            int dv = scrBotones.dv;

            if (dv == 1) Time.timeScale = 2.5f;
            else if (dv == 2) Time.timeScale = 5;
            else { Time.timeScale = 1; }

            pauseMenu.SetActive(false);
        }
    }
}
