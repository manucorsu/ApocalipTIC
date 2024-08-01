using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pfbsEnemigos; /*cada tipo de enemigo es un prefab y 
                                                 * está en este array*/
    [SerializeField] GameObject[] spawners;

    public bool spawnear;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (GameObject spawner in spawners)
        //{
        //    spawner.GetComponent<SpriteRenderer>().enabled = false;
        //}
        InvokeRepeating(nameof(SpawnEnemy), 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        if (spawnear == true)
        {
            byte rie = System.Convert.ToByte(Random.Range(0, pfbsEnemigos.Length)); //RIE = Random Index para el array de Enemigos™
            GameObject nuevoEnemigo = pfbsEnemigos[rie];

            byte ris = System.Convert.ToByte(Random.Range(0, spawners.Length)); //RIS = Random Index para el array de Spawners™
            Transform loc = spawners[ris].transform;
            Instantiate(nuevoEnemigo, loc.position, Quaternion.identity);
            EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
            Debug.Log($"s[r].n {spawners[ris].name}");
            enemigoScript.spName = "";
            enemigoScript.spName = spawners[ris].name;
        }
    }
}