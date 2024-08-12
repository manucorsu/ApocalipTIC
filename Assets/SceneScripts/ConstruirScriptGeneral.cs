using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstruirScriptGeneral : MonoBehaviour
{

    //Objetos

    public GameObject spawner;
    public EnemySpawner scrEnemySpawner;
    public GameObject[] tiles;
    public GameObject[] consumiblesZonas;
    public Text plataActualtxt;

    //Variables

    public float plataActual;

    // Start is called before the first frame update
    void Start()
    {
        plataActual = 1000;
        plataActualtxt.text = "$" + plataActual.ToString();

        scrEnemySpawner = spawner.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scrEnemySpawner.spawnear == true)
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
                    zona.gameObject.SetActive(true);
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
                    zona.gameObject.SetActive(false);
                }
            }
        }

        plataActualtxt.text = "$" + plataActual.ToString();

    }
}
