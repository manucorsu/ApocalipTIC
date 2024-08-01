using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public float spd;
    public string spName;
    public bool puedeMoverse = true;
    [SerializeField] GameObject waypoints;
    public float vida;

    // Start is called before the first frame update
    void Start()
    {
        BuscarPath();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BuscarPath()
    {
        /*jajaja no puedo usar switch porque apareció en 2019 y unity 2018 no lo acepta 
         jajajaja*/

        if (spName == "A1" || spName == "A4" || spName == "A7")
        {
            string[] camino = new string[] {"G1"};
            HacerCamino(camino);
        }
        else if (spName == "A2" || spName == "A5" || spName == "A8")
        {

        }
        else if (spName == "A3" || spName == "A6" || spName == "A9")
        {
        }
        else if (spName == "B1" || spName == "B3" || spName == "B5")
        {

        }
        else if (spName == "B2" || spName == "B4" || spName == "B6")
        {

        }
        else if (spName == "C1" || spName == "C2" || spName == "C3")
        {

        }
        else if (spName == "C4" || spName == "C5" || spName == "C6")
        {

        }
        else if (spName == "D1" || spName == "D3" || spName == "D5")
        {

        }
        else if (spName == "D2" || spName == "D4" || spName == "D6")
        {

        }
    }

    private void HacerCamino(string[] camino)
    {
        foreach (string wpName in camino)
        {
            Debug.LogError(wpName);
        }
    }
}