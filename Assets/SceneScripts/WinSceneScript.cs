using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
#if UNITY_EDITOR
            Debug.Log("Se debería cerrar el juego, pero como estás en el editor eso no sucedió.");
#endif
            Application.Quit();
        }
    }
}
