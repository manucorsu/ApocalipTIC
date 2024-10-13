using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenu; //panel PauseMenu dentro del canvas PauseCanvas
    [SerializeField] private TMP_Text txtEnemiesKilled;
    [SerializeField] private TMP_Text txtRonda;
    [SerializeField] private TMP_Text txtEnemigosRestantes;
    private byte botsASpawnear = 0;

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
            txtEnemiesKilled.text = EnemySpawner.botsEliminados.ToString();
            txtRonda.text = $"{EnemySpawner.ronda}/30";
            if (EnemySpawner.ronda == 15 || EnemySpawner.ronda == 30)
            {
                txtEnemigosRestantes.text = "al Jefe";
            }
            else
            {
                botsASpawnear = EnemySpawner.EnemyFormula(r: EnemySpawner.ronda);
                byte enemigosRestantes = System.Convert.ToByte(botsASpawnear - EnemySpawner.botsEliminadosRonda);
                txtEnemigosRestantes.text = $"{enemigosRestantes} más";
            }
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
