using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PauseScript : MonoBehaviour
{
    public static PauseScript Instance { get; private set; }
    public bool canPause = false;
    public bool IsPaused { get; private set; } = false;
    public byte botsRonda = 6; // equivalente a botsASpawnear de EnemySpawner.

    [SerializeField] private GameObject pauseMenu; //panel PauseMenu dentro del canvas PauseCanvas
    [SerializeField] private TMP_Text txtEnemiesKilled;
    [SerializeField] private TMP_Text txtRonda;
    [SerializeField] private TMP_Text txtEnemigosRestantes;
    [SerializeField] private Button btnSfxToggler;
    [SerializeField] private Button btnMusToggler;
    [SerializeField] private GameObject menuConfirmDialogBg;
    [SerializeField] private LevelChanger levelChanger;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
    }
    private void Start()
    {
        try
        {
            Dictionary<string, bool> soundStates = SoundManager.Instance.GetStates();
            Sprite onSpr = SoundManager.Instance.onSpr;
            Sprite offSpr = SoundManager.Instance.offSpr;

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
        catch (System.NullReferenceException)
        {
            Debug.LogError("NullReferenceExeception: Probablemente el singleton de SoundManager fue null. NUNCA inicies Game directamente, siempre pasá por Inicio primero.");
            Application.Quit();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!IsPaused);
        }
    }

    public void SetPause(bool s)
    // pausa/despausa el juego, y activa el panel de pausa
    // como quería que se note que el juego está pausado cuando aparece una messagebox,
    // pero no quería hacer un panel nuevo que sea igual al fondo el panel este, reciclo,
    // usando este panel desactivando los controles (ver definición de controles) si mb == true
    {
        if (canPause)
        {
            IsPaused = s;
            SoundManager.Instance.StopAllSfxLoops();
            SoundManager.Instance.PlayUIClick();
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
                    int enemigosRestantes = botsRonda - EnemySpawner.botsEliminadosRonda;
                    txtEnemigosRestantes.text = $"{enemigosRestantes} más";
                }
                Time.timeScale = 0;
            }
            else
            {
                if (menuConfirmDialogBg.activeInHierarchy) menuConfirmDialogBg.SetActive(false);
                int dv = scrBotones.dv;

                if (dv == 1) Time.timeScale = 2.5f;
                else if (dv == 2) Time.timeScale = 5;
                else Time.timeScale = 1;

                pauseMenu.SetActive(false);
            }
        }
    }

    public void ToggleSfxMenuPausa()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.Instance.ToggleSFX(btnImg);
            SoundManager.Instance.PlayUIClick();
        }
    }

    public void ToggleMusMenuPausa()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.Instance.PlayUIClick();
            SoundManager.Instance.ToggleMus(btnImg);
        }
    }

    public void EnableMenuConfirmDialog(bool stat)
    {
        SoundManager.Instance.PlayUIClick();
        menuConfirmDialogBg.SetActive(stat);
    }

    public void ReturnToMenu()
    {
        foreach (GameObject enemigo in EnemySpawner.botsVivos)
        {
            enemigo.GetComponent<EnemigoScript>().spd = 0;
        }
        Time.timeScale = 1;
        SoundManager.Instance.PlayUIClick();
        levelChanger.FadeTo("Inicio", 2);
    }
}
