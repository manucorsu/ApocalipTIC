using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstruirScriptGeneral : MonoBehaviour
{

    public GameObject spawner;
    public EnemySpawner scrEnemySpawner;
    public GameObject[] tiles;
    public GameObject[] consumiblesZonas;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
