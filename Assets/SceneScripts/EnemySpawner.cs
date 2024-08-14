using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public bool spawnear = false;

    [Header("Arrays")]
    [SerializeField] private GameObject[] pfbsEnemigos; //cada tipo de enemigo es un prefab y está en este array
    [SerializeField] private GameObject[] spawners;

    [Header("Sistema de rondas")]
    [SerializeField] private Text txtRonda;
    [SerializeField] private GameObject btnIniciarRonda; // No sé por qué no anda .enabled si es Button así que voy a usar SetActive 🤷
    [SerializeField] private float dificultad = 0.75f; //scaler de dificultad
    [SerializeField] private byte r1Bots = 6; //bots de la ronda 1, usados de base para todo el resto de las rondas
    [SerializeField] private float bps = 0.5f; //bots por segundo
    private byte ronda = 1;
    private float tiempoDesdeUltimoSpawn;
    public List<GameObject> botsVivos = new List<GameObject>();
    private byte botsASpawnear;

    void Start()
    {
        botsVivos.Clear();
        ToggleSpawning(false);
    }

    void Update()
    {
        if (spawnear == true)
        {
            tiempoDesdeUltimoSpawn += Time.deltaTime;

            if (tiempoDesdeUltimoSpawn >= (1f / bps) && botsASpawnear > 0)
            {
                SpawnEnemy();
            }
            if (botsVivos.Count <= 0 && botsASpawnear <= 0)
            {
                TerminarRonda();
            }
        }
    }
    public void EmpezarRonda()
    {
        ToggleSpawning(true);
        botsASpawnear = (byte)Mathf.Round(r1Bots * Mathf.Pow(ronda, dificultad));
    }
    private void SpawnEnemy()
    {
        GameObject prefabElegido;
        while (false != true)
        {
            byte rie = (byte)Random.Range(0, pfbsEnemigos.Length); //RIE = Random Index para el array de Enemigos™
            prefabElegido = pfbsEnemigos[rie];
            if (prefabElegido.GetComponent<EnemigoScript>().minRonda <= ronda)
            {
                break;
            }
        }

        Transform loc;
        byte ris;
        while (false != true)
        {
            ris = (byte)Random.Range(0, spawners.Length); //RIS = Random Index para el array de Spawners™
            loc = spawners[ris].transform;
            if (loc.name[0] == 'A')
            {
                if (ronda >= 5)
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        GameObject nuevoEnemigo = Instantiate(prefabElegido, loc.position, Quaternion.identity);

        EnemigoScript enemigoScript = nuevoEnemigo.GetComponent<EnemigoScript>();
        enemigoScript.spName = spawners[ris].name;

        botsASpawnear--;
        botsVivos.Add(nuevoEnemigo);
        tiempoDesdeUltimoSpawn = 0;
    }

    public void TerminarRonda()
    {
        botsVivos.Clear();
        ronda++;
        ToggleSpawning(false);
        tiempoDesdeUltimoSpawn = 0;
    }

    private void ToggleSpawning(bool t)
    {
        btnIniciarRonda.SetActive(!t);
        if (t == true)
        {
            txtRonda.text = "";
        }
        else
        {
            txtRonda.text = $"Empezando RONDA {ronda}/30";
        }
        spawnear = t;
    }
}