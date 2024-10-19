using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    public static byte r1Bots = 6;
    public static byte ronda = 1;
    public static float dificultad = 0.75f;

    [SerializeField] private GameObject pauseMenu; //panel PauseMenu dentro del canvas PauseCanvas
    [SerializeField] private TMP_Text txtEnemiesKilled;
    [SerializeField] private TMP_Text txtRonda;
    [SerializeField] private TMP_Text txtEnemigosRestantes;
    [SerializeField] private Button btnSfxToggler;
    [SerializeField] private Button btnMusToggler;
    [SerializeField] private GameObject menuConfirmDialogBg;
    [SerializeField] private LevelChanger levelChanger;

    private byte botsASpawnear = 0;

    private void Start()
    {
        try
        {
            Dictionary<string, bool> soundStates = SoundManager.instance.GetStates();
            Sprite onSpr = SoundManager.instance.onSpr;
            Sprite offSpr = SoundManager.instance.offSpr;

            if (soundStates["sfxOn"])
            {
                btnSfxToggler.image.sprite = onSpr;
            }
            else btnSfxToggler.image.sprite = offSpr;

            if (soundStates["musOn"])
            {
                btnMusToggler.image.sprite = onSpr;
            }
            else btnMusToggler.image.sprite = offSpr;
        }
        catch (NullReferenceException)
        {
            Debug.LogError("NullReferenceExeception: Probablemente el singleton de SoundManager fue null.\n" +
                        "NUNCA inicies Game directamente, siempre pasá por Inicio primero.");
        }
    }

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
                botsASpawnear = EnemySpawner.EnemyFormula(r1Bots, ronda, dificultad);
                int enemigosRestantes = botsASpawnear - EnemySpawner.botsEliminadosRonda;
                txtEnemigosRestantes.text = $"{enemigosRestantes} más";
            }
            Time.timeScale = 0;
        }
        else
        {
            int dv = scrBotones.dv;

            if (dv == 1) Time.timeScale = 2.5f;
            else if (dv == 2) Time.timeScale = 5;
            else Time.timeScale = 1;

            pauseMenu.SetActive(false);
        }
    }

    public void ToggleSfxMenuPausa()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.instance.ToggleSFX(btnImg);
            SoundManager.instance.PlayUIClick();
        }
    }

    public void ToggleMusMenuPausa()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.instance.PlayUIClick();
            SoundManager.instance.ToggleMus(btnImg);
        }
    }

    public void EnableMenuConfirmDialog(bool stat)
    {
        SoundManager.instance.PlayUIClick();
        menuConfirmDialogBg.SetActive(stat);
    }

    public void ReturnToMenu()
    {
        foreach(GameObject enemigo in EnemySpawner.botsVivos)
        {
            enemigo.GetComponent<EnemigoScript>().spd = 0;
        }
        Time.timeScale = 1;
        SoundManager.instance.PlayUIClick();
        levelChanger.FadeTo("Inicio", 2);
    }
}
