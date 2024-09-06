using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstruirScriptGeneral : MonoBehaviour
{
    //Objetos

    private EnemySpawner enemySpawner;
    public GameObject[] tiles;
    public GameObject[] consumiblesZonas;
    public TMP_Text plataActualtxt;

    //Variables

    public float plataActual;

    // Start is called before the first frame update
    void Start()
    {
        plataActual = 1000;
        plataActualtxt.text = "$" + plataActual.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.spawnear == true)
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    tile.gameObject.SetActive(false);
                }
            }

            foreach (GameObject zona in consumiblesZonas)
            {
                if (zona != null)
                {
                    zona.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject tile in tiles)
            {
                if (tile != null)
                {
                    tile.gameObject.SetActive(true);
                }
            }

            foreach (GameObject zona in consumiblesZonas)
            {
                if (zona != null)
                {
                    zona.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        plataActualtxt.text = "$" + plataActual.ToString();
    }
}
