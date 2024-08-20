using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int puntaje;
    public byte vidas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeScene("Resultados");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeScene("Creditos");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeScene("MenuInicial");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            puntaje = 0; vidas = 3;
            ChangeScene("Game");
        }
    }
    public void ChangeScene(string sceneName)
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"Ya estás en {sceneName}.");
        }
    }
}
