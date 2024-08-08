using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstruirScriptGeneral : MonoBehaviour
{

    public GameObject spawner;
    public EnemySpawner scrEnemySpawner;
    public GameObject[] tiles;

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
        }
    }
}
