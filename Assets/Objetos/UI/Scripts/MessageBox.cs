using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public static MessageBox Instance { get; private set; }
    public bool CheatsEnabled { get; private set; } = false; // perdón Jero pero no voy a hacer un GameManager a esta altura
    [SerializeField] private GameObject bg;
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtMessage;
    private float previousTimeScale = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        CheatsEnabled = false;

        if (bg == null || txtTitle == null || txtMessage == null)
        {
            throw new UnassignedReferenceException(
            "Por lo menos 1 de los componentes de la MessageBox (bg, txtTitle, txtMessage) fue null!!"
            );
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            CheatsEnabled = !CheatsEnabled;
            if (CheatsEnabled == true)
            {
                Show("Tramposo", "Activaste los cheats. 0 para atrasar ronda, 1 para adelantar, 4 para recibir $1000. Solo podés usarlos cuando no hay enemigos.");
            }
            else
            {
                Show("Así me gusta más", "Desactivaste los cheats. Gracias por jugar de verdad.");
            }
        }
    }

    public void Show(string title, string message)
    {
        if (!LevelChanger.Fading)
        {
            bg.SetActive(true);
            txtTitle.text = title;
            txtMessage.text = message;

            SoundManager.Instance.PlayUIClick();
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    }

    public void Close()
    {
        Time.timeScale = previousTimeScale;
        SoundManager.Instance.PlayUIClick();
        bg.SetActive(false);
    }
}
