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
        SoundManager.instance.PlayUIClick();
        levelChanger.FadeTo("Game", 0.5f);
    }

    public void OpenAjustes()
    {
        SoundManager.instance.PlayUIClick();
        settingsBgImg.SetActive(true);
    }
    public void ToggleSfxAjustes()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.instance.ToggleSFX(btnImg);
            SoundManager.instance.PlayUIClick();
        }
    }
    public void ToggleMusAjustes()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        if(clickedBtn != null)
        {
            Image btnImg = clickedBtn.GetComponent<Image>();
            SoundManager.instance.PlayUIClick();
            SoundManager.instance.ToggleMus(btnImg);
        }
    }

    public void CloseAjustes()
    {
        SoundManager.instance.PlayUIClick();
        settingsBgImg.SetActive(false);
    }
}
