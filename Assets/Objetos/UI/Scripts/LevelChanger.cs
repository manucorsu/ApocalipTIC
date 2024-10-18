using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private Animator animator;
    private string lvlName;

    private void Start() => StartCoroutine(InitialFade());
    private IEnumerator InitialFade()
    {
        parentCanvas.sortingOrder = 999;
        yield return new WaitForSeconds(1); 
        parentCanvas.sortingOrder = -1;
    }
    public void FadeTo(string sceneName, float spd = 1)
    {
        parentCanvas.sortingOrder = 999;
        lvlName = sceneName;
        animator.speed = spd;
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeDone()
    {
        SceneManager.LoadScene(lvlName);
    }
}
