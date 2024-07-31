using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] pfbsEnemigos; //cada tipo de enemigo es un prefab y está en este array
    public GameObject[] spawners;
    public bool spawnear;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        if (spawnear == true)
        {
            Instantiate(pfbsEnemigos[0], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}