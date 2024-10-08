﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMapaScript : MonoBehaviour
{

    //Objetos

    public GameObject[] decoraciones;

    //Variables

    public List<GameObject> sector1 = new List<GameObject>();
    public List<GameObject> sector2 = new List<GameObject>();
    public List<GameObject> sector3 = new List<GameObject>();
    public List<GameObject> sector4 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

       
        for (int i = 0; i < 4; i++)
        {
            int valorAzar1 = Random.Range(0, sector1.Count);
            Instantiate(decoraciones[Random.Range(0, decoraciones.Length)], sector1[valorAzar1].transform.position, Quaternion.identity);
            Destroy(sector1[valorAzar1]);
            sector1.RemoveAt(valorAzar1);
        }

        for (int i = 0; i < 4; i++)
        {
            int valorAzar2 = Random.Range(0, sector2.Count);
            Instantiate(decoraciones[Random.Range(0, decoraciones.Length)], sector2[valorAzar2].transform.position, Quaternion.identity);
            Destroy(sector2[valorAzar2]);
            sector2.RemoveAt(valorAzar2);
        }

        for (int i = 0; i < 4; i++)
        {
            int valorAzar3 = Random.Range(0, sector3.Count - 1);
            Instantiate(decoraciones[Random.Range(0, decoraciones.Length)], sector3[valorAzar3].transform.position, Quaternion.identity);
            Destroy(sector3[valorAzar3]);
            sector3.RemoveAt(valorAzar3);
        }

        for (int i = 0; i < 4; i++)
        {
            int valorAzar4 = Random.Range(0, sector4.Count);
            Instantiate(decoraciones[Random.Range(0, decoraciones.Length)], sector4[valorAzar4].transform.position, Quaternion.identity);
            Destroy(sector4[valorAzar4]);
            sector4.RemoveAt(valorAzar4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
