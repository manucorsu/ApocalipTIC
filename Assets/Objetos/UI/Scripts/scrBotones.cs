using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBotones : MonoBehaviour
{
    public GameObject construir;
    private ConstruirScript scrConstruir;
    public GameObject[] tiles;
    public GameObject[] torretas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(int torreta)
    {
        if (torreta == 1)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[0];
                }
            }
        }

        if (torreta == 2)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[1];
                }
            }
        }

        if (torreta == 3)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[2];
                }
            }
        }

        if (torreta == 4)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    scrConstruir = tile.GetComponent<ConstruirScript>();
                    scrConstruir.torretaSeleccionada = torretas[3];
                }
            }
        }
    }
}
