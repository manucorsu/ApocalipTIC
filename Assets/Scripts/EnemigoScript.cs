using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public float spd;
    public string spName;
    public bool puedeMoverse = false;
    [SerializeField] private GameObject[] waypoints;
    private List<Vector3> v3Camino = new List<Vector3>();

    public float hp;

    // Start is called before the first frame update
    void Start()
    {
        BuscarPath();
    }

    private void BuscarPath()
    {
        /*jajaja no puedo usar switch porque apareció en 2019 y unity 2018 no lo acepta 
         jajajaja
        
        Si no se entiende nada, ver SPAWNERSGUIDE*/

        if (spName == "A1" || spName == "A4" || spName == "A7")
        {
            V3ify(new string[] { "G1" });
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
    private void V3ify(string[] camino)
    {
        foreach (GameObject wp in waypoints)
        {
            Debug.Log(wp.name);
            if (camino.Contains(wp.name))
            {
                v3Camino.Add(wp.transform.position);
            }
        }
        puedeMoverse = true;
    }

    void Update()
    {
        if (puedeMoverse == true)
        {
            for (byte i = 0; i < v3Camino.Count; i++)
            {
                this.transform.position = Vector3.MoveTowards
                    (this.transform.position, v3Camino[i], spd * Time.deltaTime);
            }
        }
    }
}