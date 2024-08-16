using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private GameObject pauseMenu; //panel PauseMenu
    private GameObject controls; //las cosas que en winforms se llamaban controles (botones, texto) de este panel
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }
    }

    public void SetPause(bool s, bool mb = false)
    // pausa/despausa el juego, y activa el panel de pausa
    // como quería que se note que el juego está pausado cuando aparece una messagebox,
    // pero no quería hacer un panel nuevo que sea igual al fondo el panel este, reciclo,
    // usando este panel desactivando los controles (ver definición de controles) si mb == true
    {
        
        isPaused = s;
        if (s == true)
        {
            pauseMenu.SetActive(true);
            if (mb == true) controls.SetActive(false);
            else { controls.SetActive(false); }
            Time.timeScale = 0;
        }
        else
        {
            bool dv = GameObject.Find("SCENESCRIPTS").GetComponent<scrBotones>().dv;

            if (dv == true) Time.timeScale = 2;
            else { Time.timeScale = 1; }

            pauseMenu.SetActive(false);
        }
    }
}
