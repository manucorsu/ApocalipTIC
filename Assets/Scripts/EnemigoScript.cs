using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    private Vector3 initialPos;
    public GameObject[] waypoints;
    public string spName;
    private bool puedeMoverse = true;
    public float vida;

    // Start is called before the first frame update
    void Start()
    {
        DeterminarPath();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void DeterminarPath()
    {
        if(spName == "A1" || spName == "A4" || spName == "A7")
        {
            Debug.Log("A147");
        }
        else if(spName == "A2" || spName == "A5" || spName == "A8")
        {
            Debug.Log("A258");
        }
        else if(spName == "A3" || spName == "A6" || spName == "A9")
        {
            Debug.Log("A369");
        }
        else if(spName == "B1" || spName == "B3" || spName == "B5")
        {
            Debug.Log("B135");
        }
        else if(spName == "B2" || spName == "B4" || spName == "B6")
        {
            Debug.Log("B246");
        }
        else if (spName == "C1" || spName == "C2" || spName == "C3")
        {
            Debug.Log("C123");
        }
        else if(spName == "C4" || spName == "C5" || spName == "C6")
        {
            Debug.Log("C456");
        }
        else if(spName == "D1" || spName == "D3" || spName == "D5")
        {
            Debug.Log("D135");
        }
        else if(spName == "D2" || spName == "D4" || spName == "D6")
        {
            Debug.Log("D246");
        }
        else
        {
            Debug.LogWarning("Enemigo no apareció en un spawner válido");
        }
    }
}