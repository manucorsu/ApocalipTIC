using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoScript : MonoBehaviour
{
    public float hp;

    public float spd;
    [HideInInspector] public bool puedeMoverse = false;

    public string spName;

    private GameObject padreWaypoints;
    private List<Transform> waypoints = new List<Transform>();
    private List<Vector3> v3Camino = new List<Vector3>();
    byte wi = 0; //waypoint index



    // Start is called before the first frame update
    void Start()
    {
        padreWaypoints = GameObject.Find("PadreWaypoints");
        foreach (Transform hijo in padreWaypoints.transform)
        {
            if (hijo != padreWaypoints)
            {
                if (hijo.GetComponent<SpriteRenderer>().enabled)
                {
                    hijo.GetComponent<SpriteRenderer>().enabled = false;
                }
                waypoints.Add(hijo.transform);
            }
        }
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
            V3ify(new string[] { "G2" });
        }
        else if (spName == "A3" || spName == "A6" || spName == "A9")
        {
            V3ify(new string[] { "G3" });
        }
        else if (spName == "B1" || spName == "B3" || spName == "B5")
        {
            V3ify(new string[] { "W1", "W5", "G3" });
        }
        else if (spName == "B2" || spName == "B4" || spName == "B6")
        {
            V3ify(new string[] { "W2", "W5", "G3" });
        }
        else if (spName == "C1" || spName == "C2" || spName == "C3")
        {
            V3ify(new string[] { "W5", "G3" });
        }
        else if (spName == "C4" || spName == "C5" || spName == "C6")
        {
            V3ify(new string[] { "W6", "G2" });
        }
        else if (spName == "D1" || spName == "D3" || spName == "D5")
        {
            V3ify(new string[] { "W4", "W6", "G2" });
        }
        else if (spName == "D2" || spName == "D4" || spName == "D6")
        {
            V3ify(new string[] { "W3", "W6", "G2" });
        }
    }
    private void V3ify(string[] camino)
    {
        for (int i = 0; i < camino.Length; i++)
        {
            string targetName = camino[i];

            for (int j = 0; j < waypoints.Count; j++)
            {
                if (waypoints[j].name == targetName)
                {
                    v3Camino.Add(waypoints[j].position);
                    break;
                }
            }
            puedeMoverse = true;
        }
    }

    void Update()
    {
        if (puedeMoverse == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, v3Camino[wi], spd * Time.deltaTime);

            if (transform.position == v3Camino[wi])
            {
                wi++;
            }

            if (wi == v3Camino.Count)
            {
                spd = 0;
                Perder();
            }
        }
    }
    void Perder()
    {
        Debug.Log("boo hoo");
        SceneManager.LoadScene("GameOver");
    }
}