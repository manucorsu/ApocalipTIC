using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsGanaste : MonoBehaviour
{
    public void ShowGanasteTxt()
    {
        GetComponent<Animator>().enabled = false;
        WinSceneManager.Instance.ShowGanasteTxt();
    }
}
