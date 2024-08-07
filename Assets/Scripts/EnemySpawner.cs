using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool spawnear = false;

    [Header("Arrays")]
    [SerializeField] GameObject[] pfbsEnemigos; //cada tipo de enemigo es un prefab y está en este array
    [SerializeField] GameObject[] spawners;

    [Header("Sistema de rondas")]
    [SerializeField] float dificultad = 0.75f; //scaler de dificultad
    [SerializeField] byte r1Bots = 6; //bots de la ronda 1, usados de base para todo el resto de las rondas
    [SerializeField] float bps = 0.5f; //bots por segundo

    byte ronda = 1;
    float tiempoDesdeUltimoSpawn;
    [HideInInspector] public byte botsVivos;
    byte botsASpawnear;

    void Start()
    {
        foreach (GameObject spawner in spawners)
        {
            if (spawner.GetComponent<SpriteRenderer>().enabled == true)
            {
                spawner.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        EmpezarRonda();
    }
    void Update()
    {
        if (spawnear == false) { return; }
        tiempoDesdeUltimoSpawn += Time.deltaTime;
        if (tiempoDesdeUltimoSpawn >= (1f / bps) && botsASpawnear > 0)
        {
            SpawnEnemy();
        }
    }
    void EmpezarRonda()
    {
        spawnear = true;
        botsASpawnear = (byte)Mathf.Round(r1Bots * Mathf.Pow(ronda, dificultad));
    }
    void SpawnEnemy()
    {
        botsASpawnear--;
        botsVivos++;
        tiempoDesdeUltimoSpawn = 0;
        
        byte rie = (byte)Random.Range(0, pfbsEnemigos.Length); //RIE = Random Index para el array de Enemigos™
        GameObject prefabElegido = pfbsEnemigos[rie];

        byte ris = (byte)Random.Range(0, spawners.Length); //RIS = Random Index para el array de Spawners™
        Transform loc = spawners[ris].transform;

        GameObject nuevoEnemigo = Instantiate(prefabElegido, loc.position, Quaternion.identity);

        EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
        enemigoScript.spName = spawners[ris].name;
    }
}