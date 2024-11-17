using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    public static WinSceneManager Instance { get; private set; }

    private bool showedGanasteText;
    [SerializeField] private TMP_Text txtFelicitaciones;
    [SerializeField] private BlinkingTMPText txtPressAny;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;

        showedGanasteText = false;
    }

    public void ShowGanasteTxt()
    {
        if (!showedGanasteText)
        {
            showedGanasteText = true;
            txtFelicitaciones.enabled = true;
        }
    }

    private void Update()
    {
        if (showedGanasteText && SoundManager.Instance.musPlayer.isPlaying == false)
        {
            if (!txtPressAny.Blinking) txtPressAny.StartBlinking();
        }

        if (Input.anyKeyDown)
        {
#if UNITY_EDITOR
            throw new System.InvalidOperationException("El juego se debería haber cerrado pero como estás en el editor eso no sucedió. Pará vos el juego ahora.");
#endif
#pragma warning disable CS0162 // Unreachable code detected
            Application.Quit();
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}
