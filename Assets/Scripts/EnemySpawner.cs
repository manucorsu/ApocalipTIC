using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pfbsEnemigos; /*cada tipo de enemigo es un prefab y 
                                                 * está en este array*/
    [SerializeField] GameObject[] spawners;

    public bool spawnear; 

    void Start()
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<SpriteRenderer>().enabled = false;
        }
        InvokeRepeating(nameof(SpawnEnemy), 0f, 1f);
    }
    void SpawnEnemy()
    {
        if (spawnear == true)
        {
            byte rie = System.Convert.ToByte(Random.Range(0, pfbsEnemigos.Length)); //RIE = Random Index para el array de Enemigos™
            GameObject prefabElegido = pfbsEnemigos[rie];

            byte ris = System.Convert.ToByte(Random.Range(0, spawners.Length)); //RIS = Random Index para el array de Spawners™
            Transform loc = spawners[ris].transform;

            GameObject nuevoEnemigo = Instantiate(prefabElegido, loc.position, Quaternion.identity);

            EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
            enemigoScript.spName = spawners[ris].name;
        }
    }
}