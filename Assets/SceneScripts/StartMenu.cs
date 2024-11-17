using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private GameObject settingsBgImg;

    public void Jugar()
    {
        SoundManager.Instance.PlayUIClick();
        levelChanger.FadeTo("Game", 0.5f);
    }

    public void OpenAjustes()
    {
        SoundManager.Instance.PlayUIClick();
        settingsBgImg.SetActive(true);
    }
    public void ToggleSfxAjustes()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.Instance.ToggleSFX(btnImg);
            SoundManager.Instance.PlayUIClick();
        }
    }
    public void ToggleMusAjustes()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.Instance.PlayUIClick();
            SoundManager.Instance.ToggleMus(btnImg);
        }
    }

    public void CloseAjustes()
    {
        SoundManager.Instance.PlayUIClick();
        settingsBgImg.SetActive(false);
    }
}
