using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneScript : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
        scrBotones.dv = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Game");
    }
}
